import { IconButton } from "@mui/material";
import { useRootStore } from "store/RootStore";
import RefreshIcon from "@mui/icons-material/Refresh";

export const RefreshButton = () => {
  const { feedStore, rssItemStore } = useRootStore();
  const handleRefreshClick = () => {
    rssItemStore.setPage(feedStore.selectedFeed.id, 0);
  };

  return (
    <IconButton onClick={handleRefreshClick}>
      <RefreshIcon style={{ color: "white" }} />
    </IconButton>
  );
};
