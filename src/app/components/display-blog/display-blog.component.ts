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
      this.blog = (await getBlog(parseInt(id))).result[0];
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

