import { Blog } from "./Blog";

export interface Topic {
    id: number;
    name: string;
    blogs: Blog[];
  }