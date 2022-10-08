import { IconButton, Toolbar, Typography } from "@mui/material";
import { observer } from "mobx-react-lite";
import MenuIcon from "@mui/icons-material/Menu";
import { AppBar } from "./AppBar";
import { ReactNode } from "react";
import ArrowBackIcon from "@mui/icons-material/ArrowBack";
import { useNavigate } from "react-router-dom";

interface HeaderProps {
  open?: boolean;
  toggleDrawer?: () => void;
  children?: ReactNode;
  title: ReactNode;
  hasBack?: boolean;
}

export const Header = observer(
  ({ open, toggleDrawer, children, title, hasBack }: HeaderProps) => {
    const navigate = useNavigate();

    const handleBackClick = () => {
      navigate("/feeds");
    };

    return (
      <AppBar position="absolute" open={open}>
        <Toolbar
          sx={{
            pr: "24px",
          }}
        >
          {toggleDrawer && (
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
          )}
          {hasBack && (
            <IconButton onClick={handleBackClick}>
              <ArrowBackIcon style={{ color: "white" }} />
            </IconButton>
          )}
          <Typography component="h1" variant="h6" color="inherit" noWrap>
            {title}
          </Typography>
          {children}
          <Typography variant="h5"  sx={{ flexGrow: 1, textAlign: 'right' }}>
            Light RSS Reader
          </Typography>
        </Toolbar>
      </AppBar>
    );
  }
);
