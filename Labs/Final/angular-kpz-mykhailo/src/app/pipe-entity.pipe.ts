import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'pipeEntity',
  standalone: true
})
export class PipeEntityPipe implements PipeTransform {

  transform(value: unknown, ...args: unknown[]): unknown {
    return null;
  }

}
