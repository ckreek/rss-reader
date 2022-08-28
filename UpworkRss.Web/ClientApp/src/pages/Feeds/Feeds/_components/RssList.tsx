import { List, Pagination } from "@mui/material";
import { observer } from "mobx-react-lite";
import { useRootStore } from "store/RootStore";
import { RssItemCardListItem } from "./RssItemCardListItem";

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

  const handleChange = (_: React.ChangeEvent<unknown>, value: number) => {
    if (feedStore.selectedFeed) {
      rssItemStore.setPage(feedStore.selectedFeed.id, value);
    }
  };

  return (
    <>
      <List>
        {items.map((x) => (
          <RssItemCardListItem key={x.id} item={x} />
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
