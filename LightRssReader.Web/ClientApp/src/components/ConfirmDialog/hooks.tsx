import { useCallback, useState } from "react";
import { ConfirmDialog } from "./ConfirmDialog";

interface UseConfirmDialogOpenParams {
  title: string;
  content: string;
  onOk: () => void;
}

export const useConfirmDialog = (): [
  open: (params: UseConfirmDialogOpenParams) => void,
  dialog: JSX.Element | undefined
] => {
  const [isOpened, setIsOpened] = useState(false);
  const [params, setParams] = useState<UseConfirmDialogOpenParams>({
    content: "",
    onOk: () => {},
    title: "",
  });

  const handleCancel = useCallback(() => {
    setIsOpened(false);
  }, []);

  const handleOk = useCallback(() => {
    setIsOpened(false);
    params.onOk();
  }, [params]);

  const open = useCallback((params: UseConfirmDialogOpenParams) => {
    setIsOpened(true);
    setParams(params);
  }, []);

  const dialog = isOpened ? (
    <ConfirmDialog
      onCancel={handleCancel}
      onOk={handleOk}
      content={params.content}
      title={params.title}
    />
  ) : undefined;

  return [open, dialog];
};
