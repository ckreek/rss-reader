import * as React from "react";
import ListItemButton from "@mui/material/ListItemButton";
import ListItemIcon from "@mui/material/ListItemIcon";
import ListItemText from "@mui/material/ListItemText";
import AssignmentIcon from "@mui/icons-material/Assignment";
import { Feed } from "store/Feed/FeedStore";
import { useRootStore } from "store/RootStore";

interface DrawerListItemProps {
  feed: Feed;
}

export const DrawerListItem = ({ feed }: DrawerListItemProps) => {
  const { feedStore } = useRootStore();
  const handleClick = () => {
    feedStore.select(feed);
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
