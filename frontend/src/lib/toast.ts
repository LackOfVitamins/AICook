import { toast, type ExternalToast } from "svelte-sonner";

export type LocalToastItem = {
  message: string,
  type: "success" | "info" | "warning" | "error" | "loading",
  options?: ExternalToast
}

export const renderToasts = (toasts: LocalToastItem[]) => {
  for(const toastItem of toasts) {
    switch(toastItem.type)
    {
      case "success":
        toast.success(toastItem.message, toastItem.options);
        break;

      case "info":
        toast.info(toastItem.message, toastItem.options)
        break;

      case "warning":
        toast.warning(toastItem.message, toastItem.options)
        break;

      case "error":
        toast.error(toastItem.message, toastItem.options)
        break;

      case "loading":
        toast.loading(toastItem.message, toastItem.options)
        break;
    }
  }
}