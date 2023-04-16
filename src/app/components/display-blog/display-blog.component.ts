import { Component } from '@angular/core';
import { Blog } from 'src/models/Blog';

@Component({
  selector: 'app-display-blog',
  templateUrl: './display-blog.component.html',
  styleUrls: ['./display-blog.component.css']
})
export class DisplayBlogComponent {
  public blog : Blog | undefined;
}
