import {
  Box,
  Button,
  Container,
  CssBaseline,
  FormControlLabel,
  IconButton,
  Switch,
  Toolbar,
} from "@mui/material";
import ChevronLeftIcon from "@mui/icons-material/ChevronLeft";
import Divider from "@mui/material/Divider";
import { Drawer, Header, RssList, SavedSearches } from "./_components";
import { useEffect, useState } from "react";
import RefreshIcon from "@mui/icons-material/Refresh";
import AddIcon from "@mui/icons-material/Add";
import { useNavigate, useParams } from "react-router-dom";
import { useRootStore } from "store/RootStore";
import { observer } from "mobx-react-lite";
import DeleteIcon from "@mui/icons-material/Delete";
import { allFeed } from "store/Feed/FeedStore";
import { reaction, runInAction } from "mobx";
import EditIcon from "@mui/icons-material/Edit";
import { useConfirmDialog } from "components";
import { useSnackbar } from "notistack";

interface FeedsRouteParams {
  feedId?: string;
}

export const Feeds = observer(() => {
  const { feedId } = useParams<keyof FeedsRouteParams>();
  const selectedFeedId = feedId ? parseInt(feedId) || 0 : 0;

  const [open, setOpen] = useState(true);
  const [openConfirmDeleteDialog, confirmDeleteDialog] = useConfirmDialog();
  const { enqueueSnackbar, closeSnackbar } = useSnackbar();
  const { feedStore, rssItemStore } = useRootStore();
  const navigate = useNavigate();

  const feed = feedStore.getFeed(selectedFeedId);

  const toggleDrawer = () => {
    setOpen(!open);
  };

  const handleRefreshClick = () => {
    if (feed) {
      rssItemStore.setPage(feed.id, 0);
    }
  };

  const handleAddFeedClick = () => {
    navigate("/feeds/create");
  };

  const handleEditFeedClick = () => {
    if (feed && feed.id !== allFeed.id) {
      navigate(`/feeds/${feed.id}/edit`);
    }
  };

  const handleCancelDeleteClick = (feedId: number) => {
    closeSnackbar(feedId);
    feedStore.restore(feedId);
  };

  const handleDeleteFeedClick = () => {
    if (feed) {
      openConfirmDeleteDialog({
        title: `Delete feed ${feed.name}`,
        content: "Are you sure you want to delete this feed?",
        onOk: async () => {
          const feedId = feed.id;
          await feedStore.delete(feedId);
          navigate(`/feeds/${allFeed.id}`)
          enqueueSnackbar("Feed deleted", {
            key: feedId,
            variant: "success",
            action: () => {
              const handleClick = () => {
                handleCancelDeleteClick(feedId);
              };

              return (
                <Button color="inherit" size="small" onClick={handleClick}>
                  Cancel
                </Button>
              );
            },
          });
        },
      });
    }
  };

  const handleShowReadChange = (event: React.ChangeEvent<HTMLInputElement>) => {
    runInAction(() => {
      rssItemStore.showRead = event.currentTarget.checked;
    });
  };

  useEffect(
    () =>
      reaction(
        () => rssItemStore.showRead,
        () => {
          rssItemStore.reload(selectedFeedId);
        }
      ),
    [selectedFeedId, rssItemStore]
  );

  useEffect(() => {
    if (selectedFeedId !== undefined) {
      rssItemStore.reload(selectedFeedId);
    }
  }, [rssItemStore, selectedFeedId]);

  return (
    <>
      <Box sx={{ display: "flex" }}>
        <CssBaseline />
        <Header
          open={open}
          toggleDrawer={toggleDrawer}
          title={`Feed | ${feed?.name}`}
        >
          <IconButton onClick={handleRefreshClick}>
            <RefreshIcon style={{ color: "white" }} />
          </IconButton>
          {feed && feed.id !== allFeed.id && (
            <IconButton onClick={handleEditFeedClick}>
              <EditIcon style={{ color: "white" }} />
            </IconButton>
          )}
          <IconButton onClick={handleAddFeedClick}>
            <AddIcon style={{ color: "white" }} />
          </IconButton>
          {feed && feed.id !== allFeed.id && (
            <IconButton onClick={handleDeleteFeedClick}>
              <DeleteIcon style={{ color: "white" }} />
            </IconButton>
          )}
          <FormControlLabel
            control={
              <Switch
                checked={rssItemStore.showRead}
                onChange={handleShowReadChange}
                color="default"
              />
            }
            label="Show read"
          />
        </Header>
        <Drawer variant="permanent" open={open}>
          <Toolbar
            sx={{
              display: "flex",
              alignItems: "center",
              justifyContent: "flex-end",
              px: [1],
            }}
          >
            <IconButton onClick={toggleDrawer}>
              <ChevronLeftIcon />
            </IconButton>
          </Toolbar>
          <Divider />
          <SavedSearches />
        </Drawer>
        <Box
          component="main"
          sx={{
            backgroundColor: (theme) => theme.palette.grey[100],
            flexGrow: 1,
            minHeight: "100vh",
          }}
        >
          <Toolbar />
          <Container maxWidth="lg" sx={{ mt: 4, mb: 4 }}>
            <RssList feedId={selectedFeedId} />
          </Container>
        </Box>
      </Box>
      {confirmDeleteDialog}
    </>
  );
});
