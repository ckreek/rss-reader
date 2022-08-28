import { ListItem } from "@mui/material";
import { RssItemCard } from "components";
import { useRef } from "react";
import { RssItem } from "store/RssItem/RssItemStore";

interface RssItemCardListItemProps {
  item: RssItem;
}

export const RssItemCardListItem = ({ item }: RssItemCardListItemProps) => {
  const ref = useRef<HTMLLIElement | null>(null);

  const handleGoToNext = async () => {
    if (ref.current) {
      ref.current.nextElementSibling?.scrollIntoView();
    }
  };
  return (
    <ListItem ref={ref}>
      <RssItemCard item={item} onGoToNext={handleGoToNext} />
    </ListItem>
  );
};
