import { Injectable } from '@angular/core';
import { User } from 'src/models/User';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  private userSource = new BehaviorSubject<User | null>(null);
  public user$ = this.userSource.asObservable();

  constructor() { }

  setUser(user: User | null) {
    this.userSource.next(user);
  }
}
