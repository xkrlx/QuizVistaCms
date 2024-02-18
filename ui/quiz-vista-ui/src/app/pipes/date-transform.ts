import { Pipe, PipeTransform } from '@angular/core';
import { DatePipe } from '@angular/common';

@Pipe({ name: 'dateTransform' })
export class DateTransform implements PipeTransform{

    format: string = 'short'

    transform(date: Date | string): string {
        date = new Date(date);

        return new DatePipe('en-EN').transform(date, this.format) ?? ''
    }

}
