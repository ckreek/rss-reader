import { ListItem } from "@mui/material";
import { RssItemCard } from "components";
import { useRef } from "react";
import { RssItem } from "store/RssItem/RssItemStore";

interface RssItemCardListItemProps {
  item: RssItem;
  feedId: number;
}

export const RssItemCardListItem = ({
  item,
  feedId,
}: RssItemCardListItemProps) => {
  const ref = useRef<HTMLLIElement | null>(null);

  const handleGoToNext = async () => {
    if (ref.current) {
      ref.current.nextElementSibling?.scrollIntoView();
    }
  };
  return (
    <ListItem ref={ref}>
      <RssItemCard item={item} onGoToNext={handleGoToNext} feedId={feedId} />
    </ListItem>
  );
};
