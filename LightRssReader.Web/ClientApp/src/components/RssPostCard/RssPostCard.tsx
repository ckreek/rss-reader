import Card from "@mui/material/Card";
import CardContent from "@mui/material/CardContent";
import Typography from "@mui/material/Typography";
import { RssPost } from "store/RssPost/RssPostStore";
import { useEffect, useRef, useState } from "react";
import { Box, Button } from "@mui/material";
import { formatDate } from "utils";
import { useRootStore } from "store/RootStore";
import styles from "./RssPostCard.module.scss";
import { observer } from "mobx-react-lite";
import ContentCopyIcon from "@mui/icons-material/ContentCopy";
import CheckIcon from "@mui/icons-material/Check";
import { useNavigate } from "react-router-dom";
import { useSnackbar } from "notistack";

interface RssPostCardProps {
  feedId: number;
  rssPost: RssPost;
  onGoToNext?: () => {};
  hasControls: boolean;
}

export const RssPostCard = observer(
  ({ rssPost, onGoToNext, feedId, hasControls }: RssPostCardProps) => {
    const navigate = useNavigate();
    const { rssPostStore } = useRootStore();
    const ref = useRef<HTMLSpanElement | null>(null);
    const [copied, setCopied] = useState(false);
    const { enqueueSnackbar, closeSnackbar } = useSnackbar();

    useEffect(() => {
      if (ref.current) {
        ref.current.innerHTML = rssPost.summary;
      }
    }, [rssPost.summary]);

    const handleCancelHideClick = async () => {
      closeSnackbar(rssPost.id);
      await rssPostStore.hide(rssPost, feedId);
    };

    const handleHideClick = async () => {
      await rssPostStore.hide(rssPost, feedId);
      enqueueSnackbar("Post hidden", {
        key: rssPost.id,
        variant: "success",
        action: () => {
          return (
            <Button
              color="inherit"
              size="small"
              onClick={handleCancelHideClick}
            >
              Cancel
            </Button>
          );
        },
      });
    };

    const handleReadClick = async () => {
      await rssPostStore.read(rssPost);
    };

    const handleCopyUrlClick = async () => {
      navigator.clipboard.writeText(rssPost.url);
      setCopied(true);
    };

    const handleViewClick = async () => {
      navigate(`/feeds/${feedId}/posts/${rssPost.id}`);
    };

    const handleGoToNextClick = async () => {
      if (onGoToNext) {
        onGoToNext();
      }
    };

    useEffect(() => {
      const timeout = setTimeout(() => {
        if (copied) setCopied(false);
      }, 500);

      return () => clearTimeout(timeout);
    }, [copied]);

    return (
      <Card sx={{ width: "100%", position: "relative" }}>
        <CardContent className={rssPost.read ? styles.read : ""}>
          <Box display="flex" alignItems="center">
            <Typography gutterBottom variant="h5" component="div">
              {rssPost.title}
            </Typography>
            <Typography variant="subtitle2">
              {", "}
              {formatDate(rssPost.publishDate)}
            </Typography>
          </Box>
          <Typography ref={ref} variant="body2">
            {rssPost.summary}
          </Typography>
        </CardContent>
        {hasControls && (
          <Box
            className={styles.overlayWrapper}
            top={0}
            right={0}
            position="absolute"
            height="100%"
            width="33.3%"
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
                  variant="contained"
                  color="primary"
                  className={styles.button}
                  onClick={handleViewClick}
                >
                  View
                </Button>
                <Button
                  variant="contained"
                  color="primary"
                  className={styles.button}
                  onClick={handleReadClick}
                >
                  Read
                </Button>
                <Button
                  variant="contained"
                  color="primary"
                  className={styles.button}
                  onClick={handleHideClick}
                >
                  Hide
                </Button>
                <Button
                  variant="contained"
                  color="primary"
                  className={styles.button}
                  onClick={handleCopyUrlClick}
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
                <Button
                  variant="contained"
                  color="primary"
                  className={styles.button}
                  onClick={handleGoToNextClick}
                >
                  Go to next
                </Button>
              </Box>
            </Box>
          </Box>
        )}
      </Card>
    );
  }
);
