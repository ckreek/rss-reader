import Card from "@mui/material/Card";
import CardContent from "@mui/material/CardContent";
import Typography from "@mui/material/Typography";
import { RssItem } from "store/RssItem/RssItemStore";
import { useEffect, useRef, useState } from "react";
import { Box, Button } from "@mui/material";
import { formatDate } from "utils";
import { useRootStore } from "store/RootStore";
import styles from "./RssItemCard.module.scss";
import { observer } from "mobx-react-lite";
import ContentCopyIcon from "@mui/icons-material/ContentCopy";
import CheckIcon from '@mui/icons-material/Check';

interface RssItemCardProps {
  item: RssItem;
}

export const RssItemCard = observer(({ item }: RssItemCardProps) => {
  const { rssItemStore, feedStore } = useRootStore();
  const ref = useRef<HTMLSpanElement | null>(null);
  const [copied, setCopied] = useState(false);

  useEffect(() => {
    if (ref.current) {
      ref.current.innerHTML = item.summary;
    }
  }, [item.summary]);

  const handleHideClick = async () => {
    await rssItemStore.hide(item, feedStore.selectedFeed.id);
  };

  const handleReadClick = async () => {
    await rssItemStore.read(item);
  };

  const copyUrlClick = async () => {
    navigator.clipboard.writeText(item.url);
    setCopied(true);
  };

  useEffect(() => {
    const timeout = setTimeout(() => {
      if (copied) setCopied(false);
    }, 500);

    return () => clearTimeout(timeout);
  }, [copied]);

  return (
    <Card sx={{ width: "100%", position: "relative" }}>
      <CardContent className={item.read ? styles.read : ""}>
        <Box display="flex" alignItems="center">
          <Typography gutterBottom variant="h5" component="div">
            {item.title.replace(" - Upwork", "")}
          </Typography>
          <Typography variant="subtitle2">
            {", "}
            {formatDate(item.publishDate)}
          </Typography>
        </Box>
        <Typography ref={ref} variant="body2">
          {item.summary}
        </Typography>
      </CardContent>
      <Box
        className={styles.overlayWrapper}
        top={0}
        right={0}
        position="absolute"
        height="100%"
        width="50%"
      >
        <Box
          className={styles.overlayContainer}
          top={0}
          right={0}
          position="absolute"
          height="100%"
          width="100%"
        >
          <Box
            className={styles.overlay}
            top={0}
            right={0}
            position="absolute"
            height="100%"
            width="100%"
          />
          <Box
            className={styles.buttonContainer}
            top={0}
            right={0}
            position="absolute"
            height="100%"
            width="100%"
            display="flex"
            justifyContent="center"
            alignItems="center"
            flexDirection="column"
          >
            <Button
              variant="outlined"
              color="primary"
              className={styles.button}
              onClick={handleReadClick}
            >
              Read
            </Button>
            <Button
              variant="outlined"
              color="primary"
              className={styles.button}
              onClick={handleHideClick}
            >
              Hide
            </Button>
            <Button
              variant="outlined"
              color="primary"
              className={styles.button}
              onClick={copyUrlClick}
            >
              Copy URL&nbsp;
              <ContentCopyIcon
                sx={{
                  display: !copied ? "block" : "none",
                }}
              />
              <CheckIcon
                sx={{
                  display: copied ? "block" : "none",
                }}
              />
            </Button>
          </Box>
        </Box>
      </Box>
    </Card>
  );
});
