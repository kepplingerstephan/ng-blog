import { Blog } from "./Blog";

export interface User {
    id: number;
    name: string;
    blogs: Blog[];
  }