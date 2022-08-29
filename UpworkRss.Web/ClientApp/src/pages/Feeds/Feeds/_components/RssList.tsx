import { List, Pagination } from "@mui/material";
import { observer } from "mobx-react-lite";
import { useRootStore } from "store/RootStore";
import { RssItemCardListItem } from "./RssItemCardListItem";

interface RssListProps {
  feedId: number;
}

export const RssList = observer(({ feedId }: RssListProps) => {
  const { rssItemStore } = useRootStore();

  const items = feedId !== undefined ? rssItemStore.getItemsByFeedId(feedId) : [];

  const page = feedId !== undefined ? rssItemStore.getPageByFeedId(feedId) : 0;

  const count = feedId !== undefined ? rssItemStore.getCountByFeedId(feedId) : 0;

  const handleChange = (_: React.ChangeEvent<unknown>, value: number) => {
    if (feedId) {
      rssItemStore.setPage(feedId, value);
    }
  };

  return (
    <>
      <List>
        {items.map((x) => (
          <RssItemCardListItem key={x.id} item={x} feedId={feedId} />
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
