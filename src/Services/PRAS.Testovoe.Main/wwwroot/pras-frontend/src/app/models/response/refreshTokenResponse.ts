import { UserResponse } from "./userResponse";

export interface RefreshTokenResponse {
  accessToken : string;
  user: UserResponse;
}
