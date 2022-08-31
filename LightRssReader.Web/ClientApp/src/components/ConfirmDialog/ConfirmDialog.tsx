import Button from "@mui/material/Button";
import Dialog from "@mui/material/Dialog";
import DialogActions from "@mui/material/DialogActions";
import DialogContent from "@mui/material/DialogContent";
import DialogContentText from "@mui/material/DialogContentText";
import DialogTitle from "@mui/material/DialogTitle";

interface ConfirmDialogProps {
  onCancel: () => void;
  onOk: () => void;
  title: string;
  content: string;
}

export const ConfirmDialog = ({
  title,
  content,
  onCancel,
  onOk,
}: ConfirmDialogProps) => {
  return (
    <Dialog open onClose={onCancel}>
      <DialogTitle>{title}</DialogTitle>
      <DialogContent>
        <DialogContentText>{content}</DialogContentText>
      </DialogContent>
      <DialogActions>
        <Button onClick={onCancel}>Cancel</Button>
        <Button onClick={onOk} autoFocus>
          Ok
        </Button>
      </DialogActions>
    </Dialog>
  );
};
