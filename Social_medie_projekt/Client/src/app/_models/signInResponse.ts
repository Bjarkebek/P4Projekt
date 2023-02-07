import { Role } from "./role";

export interface SignInResponse {
    token: string;
    loginResponse: LoginResponse
}

export interface LoginResponse {
    loginId: number;
    email: string;
    type: Role;
    user: userResponse;
}

export interface userResponse {
    userId: number;
    firstName: string;
    lastName: string;
    address: string;
    created: Date;
    posts: postResponse;
}

export interface postResponse{
    postId: number;
    userId: number;
    title: string;
    desc?: string;
    pictureURL?: string;
    likes: number;
    date: Date;
}