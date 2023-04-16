import { Injectable } from '@angular/core';
import { User } from 'src/models/User';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  public user : User | undefined;

  constructor() {
    
  }
}
