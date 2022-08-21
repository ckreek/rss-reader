import { Box, Container, IconButton, List, ListItem } from "@mui/material";
import { RssItemCard } from "./components";
import { observer } from "mobx-react-lite";
import { useRootStore } from "store/RootStore";
import { useEffect } from "react";
import RefreshIcon from "@mui/icons-material/Refresh";

const RssList = observer(() => {
  const { rssItemStore } = useRootStore();
  const { items } = rssItemStore;

  useEffect(() => {
    rssItemStore.load();
  }, [rssItemStore]);

  return (
    <List sx={{ overflowY: "auto" }}>
      {items.map((x) => (
        <ListItem key={x.id}>
          <RssItemCard item={x} />
        </ListItem>
      ))}
    </List>
  );
});

const Header = () => {
  const { rssItemStore } = useRootStore();

  const handleRefreshClick = () => {
    rssItemStore.load();
  };

  return (
    <Box height="50px" paddingX="8px">
      <IconButton onClick={handleRefreshClick}>
        <RefreshIcon />
      </IconButton>
    </Box>
  );
};

const App = () => {
  return (
    <Container
      sx={{
        backgroundColor: "rgb(207, 232, 252)",
        display: "flex",
        flexDirection: "column",
      }}
    >
      <Header />
      <RssList />
    </Container>
  );
};

export default App;
