import { User } from "./User";

export interface Blog {
    id: number;
    userId: number;
    user: User;
    topicId: number;
    topic: Topic;
    created: Date;
    updated: Date;
    content: string;
  }