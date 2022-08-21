import { Box, Container, List, ListItem } from "@mui/material";
import { RssItemCard } from "./components";
import { observer } from "mobx-react-lite";
import { useRootStore } from "store/RootStore";
import { useEffect } from "react";

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

const App = () => {
  useEffect(() => {
    console.log("1");
  }, []);

  return (
    <Container
      sx={{
        backgroundColor: "rgb(207, 232, 252)",
        display: "flex",
      }}
    >
      <Box height="50px"></Box>
      <RssList />
    </Container>
  );
};

export default App;
