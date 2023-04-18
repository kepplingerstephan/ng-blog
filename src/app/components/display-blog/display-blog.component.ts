import { Component, OnDestroy, OnInit } from '@angular/core';
import { Blog } from 'src/models/Blog';
import { ActivatedRoute, Router } from '@angular/router';
import { APIResponse } from 'src/models/APIResponse';

@Component({
  selector: 'app-display-blog',
  templateUrl: './display-blog.component.html',
  styleUrls: ['./display-blog.component.css']
})
export class DisplayBlogComponent implements OnInit {
  public blog : Blog  = {} as Blog;
  constructor(private activatedRoute : ActivatedRoute, private _router: Router) { }

  async ngOnInit(){
    var id = this.activatedRoute.snapshot.paramMap.get('id');
    console.log(id);
    if(id){
      var result = (await getBlog(parseInt(id))).result;
      console.log(result)
      console.log(Array.isArray(result));
      // if(result){
      //   this.blog.id = result.id;
      //   this.blog.userId = result.userId;
      //   this.blog.user = result.user;
      //   this.blog.topicId = result.topicId;
      //   this.blog.topic = result.topic;
      //   this.blog.title = result.title;
      //   this.blog.content = result.content;
      //   this.blog.created = result.created;
      //   this.blog.updated = result.updated;
      // }
    }
    console.log(this.blog);
  }
}

async function getBlog(id : number) {
  const response = await fetch(`https://localhost:7217/api/blog/${id}`, {
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

