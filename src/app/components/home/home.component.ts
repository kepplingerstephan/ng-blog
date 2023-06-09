import { Component, Injectable, OnChanges, OnDestroy, OnInit, SimpleChanges } from '@angular/core';
import { UserService } from 'src/app/services/user.service';
import { User } from 'src/models/User';
import { Subscription } from 'rxjs';
import { APIResponse } from 'src/models/APIResponse';
import { Blog } from 'src/models/Blog';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})

@Injectable()
export class HomeComponent implements OnInit, OnDestroy, OnChanges {
  // public user: User | null = null;
  // private userSubscription: Subscription | undefined;
  public blogs : Blog[] = [];
  constructor(private userService: UserService) { }

  async ngOnInit() {
    // this.userSubscription = this.userService.user$.subscribe(user => {
    //   this.user = user;
    //   console.log(this.user?.name);
    // });
    this.blogs = (await getBlogs()).result;
  }

  ngOnDestroy() {
    // if (this.userSubscription) {
    //   this.userSubscription.unsubscribe();
    // }
  }
  ngOnChanges(changes: SimpleChanges) {
    // if (changes['user'] && changes['user'].currentValue) {
    //   this.user = changes['user'].currentValue;
    //   console.log(this.user?.name);
    // }
  }
}

async function getBlogs() {
  const response = await fetch("https://localhost:7217/api/blog", {
    method: 'GET',
    headers: {
      Accept: 'application/json',
    }
  });

  if (!response.ok) {
    throw new Error(`Error! status: ${response.status}`);
  }

  const result = (await response.json()) as APIResponse<Blog>;
  return result;
} 
