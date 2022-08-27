import {
  Box,
  Container,
  CssBaseline,
  IconButton,
  Toolbar,
} from "@mui/material";
import ChevronLeftIcon from "@mui/icons-material/ChevronLeft";
import Divider from "@mui/material/Divider";
import { Drawer, Header, RssList, SavedSearches } from "./_components";
import { useState } from "react";
import RefreshIcon from "@mui/icons-material/Refresh";
import AddIcon from "@mui/icons-material/Add";
import { useNavigate } from "react-router-dom";
import { useRootStore } from "store/RootStore";
import { observer } from "mobx-react-lite";
import DeleteIcon from "@mui/icons-material/Delete";
import { allFeed } from "store/Feed/FeedStore";

export const Feeds = observer(() => {
  const [open, setOpen] = useState(true);
  const toggleDrawer = () => {
    setOpen(!open);
  };

  const { feedStore, rssItemStore } = useRootStore();
  const navigate = useNavigate();

  const handleRefreshClick = () => {
    rssItemStore.setPage(feedStore.selectedFeed.id, 0);
  };

  const handleAddFeedClick = () => {
    navigate("/feeds/create");
  };

  const handleDeleteFeedClick = () => {
    feedStore.delete();
  };

  return (
    <Box sx={{ display: "flex" }}>
      <CssBaseline />
      <Header
        open={open}
        toggleDrawer={toggleDrawer}
        title={`Feed | ${feedStore.selectedFeed?.name}`}
      >
        <IconButton onClick={handleRefreshClick}>
          <RefreshIcon style={{ color: "white" }} />
        </IconButton>
        <IconButton onClick={handleAddFeedClick}>
          <AddIcon style={{ color: "white" }} />
        </IconButton>
        {feedStore.selectedFeed.id !== allFeed.id && (
          <IconButton onClick={handleDeleteFeedClick}>
            <DeleteIcon style={{ color: "white" }} />
          </IconButton>
        )}
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
          backgroundColor: (theme) =>
            theme.palette.mode === "light"
              ? theme.palette.grey[100]
              : theme.palette.grey[900],
          flexGrow: 1,
          height: "100vh",
          overflow: "auto",
        }}
      >
        <Toolbar />
        <Container maxWidth="lg" sx={{ mt: 4, mb: 4 }}>
          <RssList />
        </Container>
      </Box>
    </Box>
  );
});
