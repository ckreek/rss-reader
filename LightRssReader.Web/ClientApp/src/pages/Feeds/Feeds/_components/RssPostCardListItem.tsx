import { ListItem } from "@mui/material";
import { RssPostCard } from "components";
import { useRef } from "react";
import { RssPost } from "store/RssPost/RssPostStore";

interface RssPostCardListItemProps {
  rssPost: RssPost;
  feedId: number;
}

export const RssPostCardListItem = ({
  rssPost,
  feedId,
}: RssPostCardListItemProps) => {
  const ref = useRef<HTMLLIElement | null>(null);

  const handleGoToNext = async () => {
    if (ref.current) {
      ref.current.nextElementSibling?.scrollIntoView({
        behavior: "smooth",
      });
    }
  };
  return (
    <ListItem ref={ref}>
      <RssPostCard
        rssPost={rssPost}
        onGoToNext={handleGoToNext}
        feedId={feedId}
        hasControls
      />
    </ListItem>
  );
};
