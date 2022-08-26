import {
  Box,
  Container,
  CssBaseline,
  IconButton,
  List,
  ListItem,
  Pagination,
  Toolbar,
  Typography,
} from "@mui/material";
import { RssItemCard } from "./components";
import { observer } from "mobx-react-lite";
import { useRootStore } from "store/RootStore";
import { useEffect, useState } from "react";
import RefreshIcon from "@mui/icons-material/Refresh";
import MenuIcon from "@mui/icons-material/Menu";
import { styled } from "@mui/material/styles";
import MuiDrawer from "@mui/material/Drawer";
import MuiAppBar, { AppBarProps as MuiAppBarProps } from "@mui/material/AppBar";
import ChevronLeftIcon from "@mui/icons-material/ChevronLeft";
import Divider from "@mui/material/Divider";
import { DrawerListItem } from "listItems";
import { allFeed } from "store/Feed/FeedStore";

const RssList = observer(() => {
  const { rssItemStore, feedStore } = useRootStore();

  const items = feedStore.selectedFeed
    ? rssItemStore.getItemsByFeedId(feedStore.selectedFeed.id)
    : [];

  const page = feedStore.selectedFeed
    ? rssItemStore.getPageByFeedId(feedStore.selectedFeed.id)
    : 0;

  const count = feedStore.selectedFeed
    ? rssItemStore.getCountByFeedId(feedStore.selectedFeed.id)
    : 0;

  useEffect(() => {
    if (feedStore.selectedFeed) {
      rssItemStore.reload(feedStore.selectedFeed.id);
    }
  }, [rssItemStore, feedStore.selectedFeed]);

  const handleChange = (_: React.ChangeEvent<unknown>, value: number) => {
    if (feedStore.selectedFeed) {
      rssItemStore.setPage(feedStore.selectedFeed.id, value);
    }
  };

  return (
    <>
      <List sx={{ overflowY: "auto" }}>
        {items.map((x) => (
          <ListItem key={x.id}>
            <RssItemCard item={x} />
          </ListItem>
        ))}
      </List>
      <Pagination
        count={count}
        showFirstButton
        showLastButton
        variant="outlined"
        shape="rounded"
        page={page}
        onChange={handleChange}
      />
    </>
  );
});

const RefreshButton = () => {
  const { feedStore, rssItemStore } = useRootStore();
  const handleRefreshClick = () => {
    rssItemStore.setPage(feedStore.selectedFeed.id, 0);
  };

  return (
    <IconButton onClick={handleRefreshClick}>
      <RefreshIcon style={{ color: "white" }} />
    </IconButton>
  );
};

interface AppBarProps extends MuiAppBarProps {
  open?: boolean;
}

const drawerWidth: number = 240;

const AppBar = styled(MuiAppBar, {
  shouldForwardProp: (prop) => prop !== "open",
})<AppBarProps>(({ theme, open }) => ({
  zIndex: theme.zIndex.drawer + 1,
  transition: theme.transitions.create(["width", "margin"], {
    easing: theme.transitions.easing.sharp,
    duration: theme.transitions.duration.leavingScreen,
  }),
  ...(open && {
    marginLeft: drawerWidth,
    width: `calc(100% - ${drawerWidth}px)`,
    transition: theme.transitions.create(["width", "margin"], {
      easing: theme.transitions.easing.sharp,
      duration: theme.transitions.duration.enteringScreen,
    }),
  }),
}));

const Drawer = styled(MuiDrawer, {
  shouldForwardProp: (prop) => prop !== "open",
})(({ theme, open }) => ({
  "& .MuiDrawer-paper": {
    position: "relative",
    whiteSpace: "nowrap",
    width: drawerWidth,
    transition: theme.transitions.create("width", {
      easing: theme.transitions.easing.sharp,
      duration: theme.transitions.duration.enteringScreen,
    }),
    boxSizing: "border-box",
    ...(!open && {
      overflowX: "hidden",
      transition: theme.transitions.create("width", {
        easing: theme.transitions.easing.sharp,
        duration: theme.transitions.duration.leavingScreen,
      }),
      width: theme.spacing(7),
      [theme.breakpoints.up("sm")]: {
        width: theme.spacing(9),
      },
    }),
  },
}));

const SavedSearches = observer(() => {
  const { feedStore } = useRootStore();

  useEffect(() => {
    feedStore.load();
  }, [feedStore]);

  return (
    <List component="nav">
      <DrawerListItem key={allFeed.id} feed={allFeed} />
      <Divider sx={{ my: 1 }} />
      {feedStore.feeds.map((feed) => (
        <DrawerListItem key={feed.id} feed={feed} />
      ))}
    </List>
  );
});

interface HeaderProps {
  open: boolean;
  toggleDrawer: () => void;
}

const Header = observer(({ open, toggleDrawer }: HeaderProps) => {
  const { feedStore } = useRootStore();

  return (
    <AppBar position="absolute" open={open}>
      <Toolbar
        sx={{
          pr: "24px", // keep right padding when drawer closed
        }}
      >
        <IconButton
          edge="start"
          color="inherit"
          aria-label="open drawer"
          onClick={toggleDrawer}
          sx={{
            marginRight: "36px",
            ...(open && { display: "none" }),
          }}
        >
          <MenuIcon />
        </IconButton>
        <Typography component="h1" variant="h6" color="inherit" noWrap>
          Feed | {feedStore.selectedFeed?.name}
        </Typography>
        <RefreshButton />
      </Toolbar>
    </AppBar>
  );
});

const App = () => {
  const [open, setOpen] = useState(true);
  const toggleDrawer = () => {
    setOpen(!open);
  };

  return (
    <Box sx={{ display: "flex" }}>
      <CssBaseline />
      <Header open={open} toggleDrawer={toggleDrawer} />

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
      {/* <Header /> */}
    </Box>
  );
};

export default App;
