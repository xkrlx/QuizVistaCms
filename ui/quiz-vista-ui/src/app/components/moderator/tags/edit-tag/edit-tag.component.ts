import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Tag } from 'src/app/models/tag'; // Zakładam, że istnieje model Tag
import { TagHttpService } from 'src/app/services/http/tag-http-service'; // Zakładam, że istnieje serwis TagHttpService

@Component({
  selector: 'app-edit-tag',
  templateUrl: './edit-tag.component.html',
  styleUrls: ['./edit-tag.component.css']
})
export class EditTagComponent {
  tag!: Tag;
  tagId: string = '';
  updateSuccess = false;
  errors: string[] = [];

  constructor(private tagHttpService: TagHttpService, private route: ActivatedRoute, private router: Router) {}

  ngOnInit(): void {
    this.route.params.subscribe(params => {
      this.tagId = params['id'];
      this.getTagData();
    });
  }

  getTagData(): void {
    this.tagHttpService.showTag(this.tagId).subscribe(
      (data: any) => {
        console.log(data);
        this.tag = data.model;
      },
      (error) => {
        console.error('Error fetching tag:', error);
      }
    );
  }

  updateTag(): void {
    this.tagHttpService.update(this.tag).subscribe(
      response => {
        console.log('Tag updated successfully', response);
        this.updateSuccess = true;
        this.errors = [];
        setTimeout(() => this.updateSuccess = false, 5000);
      },
      error => {
        this.updateSuccess = false;
        if (error.error && error.error.errors) {
          this.errors = Object.keys(error.error.errors).flatMap(k => error.error.errors[k]);
        } else {
          this.errors = ['Wystąpił błąd podczas aktualizacji danych tagu.'];
        }
      }
    );
  }

  deleteTag(tagId: string | undefined): void {
    if (!tagId) {
      console.error("tagId undefined");
      return;
    }

    this.tagHttpService.delete(tagId).subscribe(
      response => {
        console.log('Tag deleted successfully', response);
        this.router.navigate(['/moderator/tags']);
      },
      error => {
        if (error.error && error.error.errors) {
          this.errors = Object.keys(error.error.errors).flatMap(k => error.error.errors[k]);
        } else {
          this.errors = ['Wystąpił błąd podczas usuwania tagu.'];
        }
      }
    );
  }
}
