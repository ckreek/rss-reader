import CircularProgress from "@mui/material/CircularProgress";
import Box from "@mui/material/Box";
import { ReactNode } from "react";

interface LoadingBoxProps {
  loading: boolean;
  children: ReactNode;
}

export const LoadingBox = ({ loading, children }: LoadingBoxProps) => {
  if (loading) {
    return (
      <Box
        sx={{
          display: "flex",
          flexGrow: 1,
          justifyContent: "center",
          alignItems: "center",
        }}
      >
        <CircularProgress />
      </Box>
    );
  }

  return <>{children}</>;
};
