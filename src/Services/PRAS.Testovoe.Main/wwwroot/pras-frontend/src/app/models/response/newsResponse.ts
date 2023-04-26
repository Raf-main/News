import { NewsPreviewResponse } from "./newsPreviewResponse";

export interface NewsResponse extends NewsPreviewResponse {
  text: string;
}
