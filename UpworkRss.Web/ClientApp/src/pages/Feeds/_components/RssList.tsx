import { List, ListItem, Pagination } from "@mui/material";
import { RssItemCard } from "components";
import { observer } from "mobx-react-lite";
import { useRootStore } from "store/RootStore";
import { useEffect } from "react";

export const RssList = observer(() => {
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
