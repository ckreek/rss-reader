import Card from "@mui/material/Card";
import CardContent from "@mui/material/CardContent";
import Typography from "@mui/material/Typography";
import { RssItem } from "store/RssItem/RssItemStore";
import { useEffect, useRef } from "react";

interface RssItemCardProps {
  item: RssItem;
}

export const RssItemCard = ({ item }: RssItemCardProps) => {
  const ref = useRef<HTMLSpanElement | null>(null);

  useEffect(() => {
    if (ref.current) {
      ref.current.innerHTML = item.summary;
    }
  }, [item.summary]);

  return (
    <Card sx={{ width: "100%" }}>
      <CardContent>
        <Typography gutterBottom variant="h5" component="div">
          {item.title.replace(" - Upwork", "")}
        </Typography>
        <Typography ref={ref} variant="body2">
          {item.summary}
        </Typography>
      </CardContent>
    </Card>
  );
};
