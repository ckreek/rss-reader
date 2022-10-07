import ListItemButton from "@mui/material/ListItemButton";
import ListItemIcon from "@mui/material/ListItemIcon";
import ListItemText from "@mui/material/ListItemText";
import AssignmentIcon from "@mui/icons-material/Assignment";
import { Feed } from "store/Feed/FeedStore";
import { useNavigate } from "react-router-dom";

interface DrawerListItemProps {
  feed: Feed;
}

export const DrawerListItem = ({ feed }: DrawerListItemProps) => {
  const navigate = useNavigate();
  const handleClick = () => {
    navigate(`/feeds/${feed.id}`);
  };

  return (
    <ListItemButton onClick={handleClick}>
      <ListItemIcon>
        <AssignmentIcon />
      </ListItemIcon>
      <ListItemText primary={feed.name} />
    </ListItemButton>
  );
};
