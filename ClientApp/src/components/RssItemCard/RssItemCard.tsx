import Card from "@mui/material/Card";
import CardContent from "@mui/material/CardContent";
import Typography from "@mui/material/Typography";
import { RssItem } from "store/RssItem/RssItemStore";
import { useEffect, useRef } from "react";
import { Box, Button, CardActions } from "@mui/material";
import { formatDate } from "utils";

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
        <Box display="flex" alignItems="center">
          <Typography gutterBottom variant="h5" component="div">
            {item.title.replace(" - Upwork", "")}
          </Typography>
          <Typography variant="subtitle2">{', '}{formatDate(item.publishDate)}</Typography>
        </Box>
        <Typography ref={ref} variant="body2">
          {item.summary}
        </Typography>
      </CardContent>
      <CardActions>
        <Button size="small">Hide</Button>
      </CardActions>
    </Card>
  );
};
