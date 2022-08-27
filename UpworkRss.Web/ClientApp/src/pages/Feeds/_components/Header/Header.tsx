import { IconButton, Toolbar, Typography } from "@mui/material";
import { observer } from "mobx-react-lite";
import { useRootStore } from "store/RootStore";
import MenuIcon from "@mui/icons-material/Menu";
import { RefreshButton } from "./RefreshButton";
import { AppBar } from "./AppBar";

interface HeaderProps {
  open: boolean;
  toggleDrawer: () => void;
}

export const Header = observer(({ open, toggleDrawer }: HeaderProps) => {
  const { feedStore } = useRootStore();

  return (
    <AppBar position="absolute" open={open}>
      <Toolbar
        sx={{
          pr: "24px",
        }}
      >
        <IconButton
          edge="start"
          color="inherit"
          aria-label="open drawer"
          onClick={toggleDrawer}
          sx={{
            marginRight: "36px",
            ...(open && { display: "none" }),
          }}
        >
          <MenuIcon />
        </IconButton>
        <Typography component="h1" variant="h6" color="inherit" noWrap>
          Feed | {feedStore.selectedFeed?.name}
        </Typography>
        <RefreshButton />
      </Toolbar>
    </AppBar>
  );
});
