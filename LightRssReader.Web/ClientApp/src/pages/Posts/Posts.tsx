import { Box, Container, CssBaseline, Toolbar } from "@mui/material";
import { Header, RssPostCard } from "components";
import { observer } from "mobx-react-lite";
import { useEffect } from "react";
import { useParams } from "react-router-dom";
import { useRootStore } from "store/RootStore";

interface PostsRouteParams {
  feedId: string;
  postId: string;
}

export const Posts = observer(() => {
  const params = useParams<keyof PostsRouteParams>();
  const { rssPostStore } = useRootStore();

  const rssPost =
    params.feedId !== undefined && params.postId !== undefined
      ? rssPostStore.getRssPost(parseInt(params.feedId), parseInt(params.postId))
      : undefined;

  useEffect(() => {
    if (!rssPost) {
      // load
    }
  }, [rssPost]);

  return (
    <Box sx={{ display: "flex" }}>
      <CssBaseline />
      <Header title="Feed | Add" hasBack />
      <Box
        component="main"
        sx={{
          backgroundColor: (theme) =>
            theme.palette.mode === "light"
              ? theme.palette.grey[100]
              : theme.palette.grey[900],
          flexGrow: 1,
          height: "100vh",
          overflow: "auto",
        }}
      >
        <Toolbar />
        <Container maxWidth="lg" sx={{ mt: 4, mb: 4 }}>
          <Box
            sx={{
              alignItems: "center",
              display: "flex",
              flexDirection: "column",
            }}
          >
            {rssPost && <RssPostCard rssPost={rssPost} feedId={rssPost.feedId} />}
          </Box>
        </Container>
      </Box>
    </Box>
  );
});
