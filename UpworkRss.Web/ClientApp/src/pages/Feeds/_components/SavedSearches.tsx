import { List } from "@mui/material";
import { observer } from "mobx-react-lite";
import { useRootStore } from "store/RootStore";
import { useEffect } from "react";
import Divider from "@mui/material/Divider";
import { DrawerListItem } from "listItems";
import { allFeed } from "store/Feed/FeedStore";

export const SavedSearches = observer(() => {
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
