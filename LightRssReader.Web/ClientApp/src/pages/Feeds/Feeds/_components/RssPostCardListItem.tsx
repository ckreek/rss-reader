import { ListItem } from "@mui/material";
import { RssPostCard } from "components";
import { useRef } from "react";
import { RssPost } from "store/RssPost/RssPostStore";

interface RssPostCardListItemProps {
  item: RssPost;
  feedId: number;
}

export const RssPostCardListItem = ({
  item,
  feedId,
}: RssPostCardListItemProps) => {
  const ref = useRef<HTMLLIElement | null>(null);

  const handleGoToNext = async () => {
    if (ref.current) {
      ref.current.nextElementSibling?.scrollIntoView();
    }
  };
  return (
    <ListItem ref={ref}>
      <RssPostCard item={item} onGoToNext={handleGoToNext} feedId={feedId} />
    </ListItem>
  );
};
