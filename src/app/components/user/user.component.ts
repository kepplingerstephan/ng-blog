import { Component, Injectable, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError, retry } from 'rxjs/operators';
import { User } from 'src/models/User';
import { APIResponse } from 'src/models/APIResponse';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-user',
  templateUrl: './user.component.html',
  styleUrls: ['./user.component.css']
})

@Injectable()
export class UserComponent implements OnInit {
  constructor(private http: HttpClient) { }

  public users: User[] = [];
  public selectedUser : string = "";
  public selectedUserName : string ="";

  async ngOnInit() {
    this.users = (await getUsers()).result;
  }

  selectChanged(){
    var user = this.users.find(u => u.id === parseInt(this.selectedUser))
    if(user){
      this.selectedUserName = user?.name;
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
