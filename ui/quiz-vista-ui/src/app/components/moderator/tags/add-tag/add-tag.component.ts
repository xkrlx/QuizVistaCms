import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { Tag } from 'src/app/models/tag';
import { TagHttpService } from 'src/app/services/http/tag-http-service';

@Component({
  selector: 'app-add-tag',
  templateUrl: './add-tag.component.html',
  styleUrls: ['./add-tag.component.css']
})
export class AddTagComponent {
  tag = new Tag();
  createSuccess = false;
  errors: string[] = [];

  constructor(private categoryHttpService: TagHttpService, private router: Router) {}

  createTag(): void {
    this.categoryHttpService.create(this.tag).subscribe(
      response => {
        console.log('Tag created successfully', response);
        this.router.navigate(['/moderator/tags']);
      },
      error => {
        this.createSuccess = false;
        if (error.error && error.error.errors) {
          this.errors = Object.keys(error.error.errors).flatMap(k => error.error.errors[k]);
        } else {
          this.errors = ['Wystąpił błąd podczas dodawania tagu..'];
        }
      }
    );
  }
}
