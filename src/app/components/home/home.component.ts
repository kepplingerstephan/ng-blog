import { Component, Injectable, OnChanges, OnDestroy, OnInit, SimpleChanges } from '@angular/core';
import { UserService } from 'src/app/services/user.service';
import { User } from 'src/models/User';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})

@Injectable()
export class HomeComponent implements OnInit, OnDestroy, OnChanges {
  public user: User | null = null;
  private userSubscription: Subscription | undefined;

  constructor(private userService: UserService) { }

  ngOnInit() {
    this.userSubscription = this.userService.user$.subscribe(user => {
      this.user = user;
      console.log(this.user?.name);
    });
  }

  ngOnDestroy() {
    if (this.userSubscription) {
      this.userSubscription.unsubscribe();
    }
  }
  ngOnChanges(changes: SimpleChanges) {
    if (changes['user'] && changes['user'].currentValue) {
      this.user = changes['user'].currentValue;
      console.log(this.user?.name);
    }
  }
}
