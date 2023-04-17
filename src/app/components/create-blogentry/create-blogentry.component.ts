import { Component, ViewChild, AfterViewInit, OnInit} from '@angular/core';
import { User } from 'src/models/User';
import { APIResponse } from 'src/models/APIResponse';
import { Topic } from 'src/models/Topic';
import { Blog } from 'src/models/Blog';
import { HttpClient, HttpHeaders } from '@angular/common/http';

declare var tinymce: any; // Declare the tinymce global object

@Component({
  selector: 'app-create-blogentry',
  templateUrl: './create-blogentry.component.html',
  styleUrls: ['./create-blogentry.component.css'],
})
export class CreateBlogentryComponent implements OnInit, AfterViewInit{
  @ViewChild('myEditor') myEditor :any;
  public topics : Topic[] = [];
  public users : User[] = [];

  public selectedTopicId : string = "";
  public selectedTopic : Topic = {} as Topic;
  public selectedUserId : string = "";
  public selectedUser : User= {} as User;
  public title : string = "";

  constructor(private http: HttpClient) { }

  async ngOnInit(){
    this.topics = (await getTopics()).result;
    this.users = (await getUsers()).result;
  }

  ngAfterViewInit() {
    // Ensure that the TinyMCE editor has been fully initialized
    if (this.myEditor && this.myEditor.editor) {
      console.log('TinyMCE editor initialized');
    }
  }
  logContent() {
    const content = tinymce.get('myEditor').getContent();
    console.log(content);
  }

  topicSelectChanged(){
    this.selectedTopic = this.topics.find(t => t.id === parseInt(this.selectedTopicId)) as Topic; 
  }

  userSelectChanged(){
    this.selectedUser = this.users.find(u => u.id === parseInt(this.selectedUserId)) as User; 
  }

  submit(){
    var newBlog = { 
      user : this.selectedUser as User, 
      topic : this.selectedTopic as Topic, 
      topicId : this.selectedTopic.id as number,
      userId : this.selectedUser.id as number, 
      title : this.title as string,
      content : tinymce.get('myEditor').getContent(),
      created : new Date(),
      updated : new Date()
    } as Blog;
    // post
    console.log(newBlog);
    const url = "https://localhost:7217/api/blog";
    const headers = new HttpHeaders({
      'Content-Type' : 'application/json'
    });

    this.http.post(url, newBlog, {headers}).subscribe(
      response => {
        // console.log('Response: ', response);
        // document.getElementById("userSelect").selectedIndex = -1;

      },
      error => {
        console.log('Error: ', error);
      }
    );
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

async function getTopics() {
  const response = await fetch("https://localhost:7217/api/topic", {
    method: 'GET',
    headers: {
      Accept: 'application/json',
    }
  });

  if (!response.ok) {
    throw new Error(`Error! status: ${response.status}`);
  }

  const result = (await response.json()) as APIResponse<Topic>;
  return result;
}