import {
  Box,
  Button,
  Container,
  CssBaseline,
  FormControl,
  TextField,
  Toolbar,
} from "@mui/material";
import { useState } from "react";
import { useNavigate } from "react-router-dom";
import { useRootStore } from "store/RootStore";
import { Header } from "../_components";

export const FeedsCreate = () => {
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

  const handleAddClick = async () => {
    await feedStore.add({
      name,
      url,
    });
    navigate("/feeds");
  };

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
                Add
              </Button>
            </FormControl>
          </Box>
        </Container>
      </Box>
    </Box>
  );
};
