import { ErrorHandler, Inject, isDevMode, NgZone } from "@angular/core";
import { ToastrService } from 'ngx-toastr';
import * as Sentry from "@sentry/angular";

export class AppErrorHandler implements ErrorHandler {
    constructor(
        @Inject(ToastrService) private toster: ToastrService,
        private ngzone: NgZone
    ) { }

    handleError(error: any): void {
        if (!isDevMode())
            Sentry.captureException(error);
        this.ngzone.run(() => this.toster.error(`An unexpected error happened`))
    }
}