import { UserResponse } from "./userResponse";

export interface LoginResponse {
  accessToken : string;
  user: UserResponse;
}
