import { HttpInterceptorFn } from '@angular/common/http';

export const interceptorEntityInterceptor: HttpInterceptorFn = (req, next) => {
  return next(req);
};
