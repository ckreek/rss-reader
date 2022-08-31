import {
  Box,
  Button,
  Container,
  CssBaseline,
  FormControl,
  TextField,
  Toolbar,
} from "@mui/material";
import { Header } from "components";
import { useEffect, useState } from "react";
import { useNavigate, useParams } from "react-router-dom";
import { useRootStore } from "store/RootStore";

interface FeedDetailsRouteParams {
  feedId: string;
}

export const FeedDetails = () => {
  const params = useParams<keyof FeedDetailsRouteParams>();
  const navigate = useNavigate();
  const { feedStore } = useRootStore();
  const [name, setName] = useState("");
  const handleNameChange = (event: React.ChangeEvent<HTMLInputElement>) => {
    setName(event.currentTarget.value);
  };
  const [url, setUrl] = useState("");
  const handleUrlChange = (event: React.ChangeEvent<HTMLInputElement>) => {
    setUrl(event.currentTarget.value);
  };

  const feedId = params.feedId
    ? parseInt(params.feedId) || undefined
    : undefined;
  const feed = feedId !== undefined ? feedStore.getFeed(feedId) : undefined;

  useEffect(() => {
    if (feed) {
      setName(feed.name);
      setUrl(feed.url);
    }
  }, [feed]);

  useEffect(() => {
    if (feedId && !feed) {
      // load
    }
  }, [feed, feedId]);

  const handleAddClick = async () => {
    if (feedId) {
      await feedStore.update(feedId, {
        name,
        url,
      });
      navigate(`/feeds/${feedId}`);
    } else {
      const newFeed = await feedStore.add({
        name,
        url,
      });
      navigate(`/feeds/${newFeed.id}`);
    }
  };

  return (
    <Box sx={{ display: "flex" }}>
      <CssBaseline />
      <Header title="Feed" hasBack />
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
            <FormControl sx={{ m: 1, width: { xs: "100%", md: "50%" } }}>
              <TextField
                label="Name"
                variant="outlined"
                value={name}
                onChange={handleNameChange}
              />
            </FormControl>
            <FormControl sx={{ m: 1, width: { xs: "100%", md: "50%" } }}>
              <TextField
                label="Url"
                variant="outlined"
                value={url}
                onChange={handleUrlChange}
              />
            </FormControl>
            <FormControl sx={{ m: 1, width: { xs: "100%", md: "50%" } }}>
              <Button variant="contained" onClick={handleAddClick}>
                Save
              </Button>
            </FormControl>
          </Box>
        </Container>
      </Box>
    </Box>
  );
};
