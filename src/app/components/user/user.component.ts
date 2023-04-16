import { AfterViewInit, Component, Injectable, OnDestroy, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { User } from 'src/models/User';
import { APIResponse } from 'src/models/APIResponse';
import { UserService } from 'src/app/services/user.service';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-user',
  templateUrl: './user.component.html',
  styleUrls: ['./user.component.css'],
})

@Injectable()
export class UserComponent implements OnInit, OnDestroy, AfterViewInit {
  public user: User | null = null;
  private userSubscription: Subscription | undefined;

  constructor(private http: HttpClient, private userService: UserService) { }

  public selectedUser : string = "";

  public users: User[] = [];

  async ngOnInit() {
    this.users = (await getUsers()).result;
    this.userSubscription = this.userService.user$.subscribe(user => {
      this.user = user;
      // console.log(this.user?.name); // Log inside the subscription callback
    });
  }

  ngAfterViewInit(): void {
    console.log(this.user?.name); // Log user name after view has been initialized
  }

  ngOnDestroy(): void {
    if(this.userSubscription){
      this.userSubscription.unsubscribe();
    }
  }

  selectChanged(){
    if(this.selectedUser){
      const u = this.users.find(u => u.id === parseInt(this.selectedUser)) || null;
      this.userService.setUser(u);
      console.log(this.user?.name);
    }
  }
}

async function getUsers() {
  const response = await fetch("https://localhost:7217/api/user", {
    method: 'GET',
    headers: {
      Accept: 'application/json',
    }
  });

  if (!response.ok) {
    throw new Error(`Error! status: ${response.status}`);
  }

  const result = (await response.json()) as APIResponse<User>;
  return result;
} 
