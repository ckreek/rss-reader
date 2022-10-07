import { List, Pagination } from "@mui/material";
import { observer } from "mobx-react-lite";
import { useRootStore } from "store/RootStore";
import { RssPostCardListItem } from "./RssPostCardListItem";

interface RssListProps {
  feedId: number;
}

export const RssList = observer(({ feedId }: RssListProps) => {
  const { rssPostStore } = useRootStore();

  const rssPosts = feedId !== undefined ? rssPostStore.getRssPostsByFeedId(feedId) : [];

  const page = feedId !== undefined ? rssPostStore.getPageByFeedId(feedId) : 0;

  const count = feedId !== undefined ? rssPostStore.getCountByFeedId(feedId) : 0;

  const handleChange = (_: React.ChangeEvent<unknown>, value: number) => {
    if (feedId) {
      rssPostStore.setPage(feedId, value);
    }
  };

  return (
    <>
      <List>
        {rssPosts.map((x) => (
          <RssPostCardListItem key={x.id} rssPost={x} feedId={feedId} />
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
