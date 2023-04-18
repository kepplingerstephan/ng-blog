export interface APIResponse<T> {
    isSuccess: boolean;
    result: T[];
    statusCode: number;
    errorMessages: Array<string>;
  }

  export interface APIResponseById<T> {
    isSuccess: boolean;
    result: T;
    statusCode: number;
    errorMessages: Array<string>;
  }