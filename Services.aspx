<%@ Page Title="Services" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Services.aspx.cs" Inherits="Z_Wallet.Services" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <main>
        <section class="page-header bg-tertiary">
            <div class="container">
                <div class="row">
                    <div class="col-8 mx-auto text-center pt-lg-5">
                        <h2 class="mb-3 text-capitalize">Our Services</h2>
                        <ul class="list-inline breadcrumbs text-capitalize" style="font-weight: 500">
                            <li class="list-inline-item"><a href="/Default">Home</a>
                            </li>
                            <li class="list-inline-item">/ &nbsp; <a href="/Services">Services</a>
                            </li>
                        </ul>
                    </div>
                </div>
            </div>
            <div class="has-shapes">
                <svg class="shape shape-left text-light" viewBox="0 0 192 752" fill="none" xmlns="http://www.w3.org/2000/svg">
                    <path d="M-30.883 0C-41.3436 36.4248 -22.7145 75.8085 4.29154 102.398C31.2976 128.987 65.8677 146.199 97.6457 166.87C129.424 187.542 160.139 213.902 172.162 249.847C193.542 313.799 149.886 378.897 129.069 443.036C97.5623 540.079 122.109 653.229 191 728.495" stroke="currentColor" stroke-miterlimit="10" />
                    <path d="M-55.5959 7.52271C-66.0565 43.9475 -47.4274 83.3312 -20.4214 109.92C6.58466 136.51 41.1549 153.722 72.9328 174.393C104.711 195.064 135.426 221.425 147.449 257.37C168.829 321.322 125.174 386.42 104.356 450.559C72.8494 547.601 97.3965 660.752 166.287 736.018" stroke="currentColor" stroke-miterlimit="10" />
                    <path d="M-80.3302 15.0449C-90.7909 51.4697 -72.1617 90.8535 -45.1557 117.443C-18.1497 144.032 16.4205 161.244 48.1984 181.915C79.9763 202.587 110.691 228.947 122.715 264.892C144.095 328.844 100.439 393.942 79.622 458.081C48.115 555.123 72.6622 668.274 141.552 743.54" stroke="currentColor" stroke-miterlimit="10" />
                    <path d="M-105.045 22.5676C-115.506 58.9924 -96.8766 98.3762 -69.8706 124.965C-42.8646 151.555 -8.29436 168.767 23.4835 189.438C55.2615 210.109 85.9766 236.469 98.0001 272.415C119.38 336.367 75.7243 401.464 54.9072 465.604C23.4002 562.646 47.9473 675.796 116.838 751.063" stroke="currentColor" stroke-miterlimit="10" />
                </svg>
                <svg class="shape shape-right text-light" viewBox="0 0 731 746" fill="none" xmlns="http://www.w3.org/2000/svg">
                    <path d="M12.1794 745.14C1.80036 707.275 -5.75764 666.015 8.73984 629.537C27.748 581.745 80.4729 554.968 131.538 548.843C182.604 542.703 234.032 552.841 285.323 556.748C336.615 560.64 391.543 557.276 433.828 527.964C492.452 487.323 511.701 408.123 564.607 360.255C608.718 320.353 675.307 307.183 731.29 327.323" stroke="currentColor" stroke-miterlimit="10" />
                    <path d="M51.0253 745.14C41.2045 709.326 34.0538 670.284 47.7668 635.783C65.7491 590.571 115.623 565.242 163.928 559.449C212.248 553.641 260.884 563.235 309.4 566.931C357.916 570.627 409.887 567.429 449.879 539.701C505.35 501.247 523.543 426.331 573.598 381.059C615.326 343.314 678.324 330.853 731.275 349.906" stroke="currentColor" stroke-miterlimit="10" />
                    <path d="M89.8715 745.14C80.6239 711.363 73.8654 674.568 86.8091 642.028C103.766 599.396 150.788 575.515 196.347 570.054C241.906 564.578 287.767 573.629 333.523 577.099C379.278 580.584 428.277 577.567 465.976 551.423C518.279 515.172 535.431 444.525 582.62 401.832C621.964 366.229 681.356 354.493 731.291 372.46" stroke="currentColor" stroke-miterlimit="10" />
                    <path d="M128.718 745.14C120.029 713.414 113.678 678.838 125.837 648.274C141.768 608.221 185.939 585.788 228.737 580.659C271.536 575.515 314.621 584.008 357.6 587.282C400.58 590.556 446.607 587.719 482.028 563.16C531.163 529.111 547.275 462.733 591.612 422.635C628.572 389.19 684.375 378.162 731.276 395.043" stroke="currentColor" stroke-miterlimit="10" />
                    <path d="M167.564 745.14C159.432 715.451 153.504 683.107 164.863 654.519C179.753 617.046 221.088 596.062 261.126 591.265C301.164 586.452 341.473 594.402 381.677 597.465C421.88 600.527 464.95 597.872 498.094 574.896C544.061 543.035 559.146 480.942 600.617 443.423C635.194 412.135 687.406 401.817 731.276 417.612" stroke="currentColor" stroke-miterlimit="10" />
                    <path d="M817.266 289.466C813.108 259.989 787.151 237.697 759.261 227.271C731.372 216.846 701.077 215.553 671.666 210.904C642.254 206.24 611.795 197.156 591.664 175.224C555.853 136.189 566.345 75.5336 560.763 22.8649C552.302 -56.8256 498.487 -130.133 425 -162.081" stroke="currentColor" stroke-miterlimit="10" />
                    <path d="M832.584 276.159C828.427 246.683 802.469 224.391 774.58 213.965C746.69 203.539 716.395 202.246 686.984 197.598C657.573 192.934 627.114 183.85 606.982 161.918C571.172 122.883 581.663 62.2275 576.082 9.55873C567.62 -70.1318 513.806 -143.439 440.318 -175.387" stroke="currentColor" stroke-miterlimit="10" />
                    <path d="M847.904 262.853C843.747 233.376 817.789 211.084 789.9 200.659C762.011 190.233 731.716 188.94 702.304 184.292C672.893 179.627 642.434 170.544 622.303 148.612C586.492 109.577 596.983 48.9211 591.402 -3.74766C582.94 -83.4382 529.126 -156.746 455.638 -188.694" stroke="currentColor" stroke-miterlimit="10" />
                    <path d="M863.24 249.547C859.083 220.07 833.125 197.778 805.236 187.353C777.347 176.927 747.051 175.634 717.64 170.986C688.229 166.321 657.77 157.237 637.639 135.306C601.828 96.2707 612.319 35.6149 606.738 -17.0538C598.276 -96.7443 544.462 -170.052 470.974 -202" stroke="currentColor" stroke-miterlimit="10" />
                </svg>
            </div>
        </section>

        <section class="section">
            <div class="container">
                <div class="row justify-content-center">
                    <div class="icon-box-item text-center col-lg-4 col-md-6 mb-4">
                        <div class="rounded shadow py-5 px-4">
                            <div class="icon">
                                <i class="fas fa-mobile-alt"></i>
                            </div>
                            <h3 class="mb-3">Web Wallet</h3>
                            <p class="mb-4">Experience the ease and convenience of our web wallet for all your digital transactions.</p>
                            <a class="btn btn-sm btn-outline-primary" href="/FAQ">Learn More <i class="las la-arrow-right ms-1"></i></a>
                        </div>
                    </div>
                    <div class="icon-box-item text-center col-lg-4 col-md-6 mb-4">
                        <div class="rounded shadow py-5 px-4">
                            <div class="icon">
                                <i class="fas fa-money-check"></i>
                            </div>
                            <h3 class="mb-3">Online Payments</h3>
                            <p class="mb-4">Make fast and secure online payments with our trusted e-wallet platform at your finger tip.</p>
                            <a class="btn btn-sm btn-outline-primary" href="/FAQ">Learn More <i class="las la-arrow-right ms-1"></i></a>
                        </div>
                    </div>
                    <div class="icon-box-item text-center col-lg-4 col-md-6 mb-4">
                        <div class="rounded shadow py-5 px-4">
                            <div class="icon">
                                <i class="fas fa-exchange-alt"></i>
                            </div>
                            <h3 class="mb-3">Money Transfers</h3>
                            <p class="mb-4">Transfer money quickly and easily to anyone, anywhere, with our reliable e-wallet platform.</p>
                            <a class="btn btn-sm btn-outline-primary" href="/FAQ">Learn More <i class="las la-arrow-right ms-1"></i></a>
                        </div>
                    </div>
                    <div class="icon-box-item text-center col-lg-4 col-md-6 mb-4">
                        <div class="rounded shadow py-5 px-4">
                            <div class="icon">
                                <i class="fas fa-wallet"></i>
                            </div>
                            <h3 class="mb-3">E-Wallet Integration</h3>
                            <p class="mb-4">Integrate your preferred payment methods with our e-wallet platform for hassle-free transactions. Access your funds easily and securely.</p>
                            <a class="btn btn-sm btn-outline-primary" href="/FAQ">Learn More <i class="las la-arrow-right ms-1"></i></a>
                        </div>
                    </div>
                    <div class="icon-box-item text-center col-lg-4 col-md-6 mb-4">
                        <div class="rounded shadow py-5 px-4">
                            <div class="icon">
                                <i class="fas fa-lock"></i>
                            </div>
                            <h3 class="mb-3">Security and Support</h3>
                            <p class="mb-4">Our top priority is the safety and security of your funds and personal information. Our 24/7 customer support team is always available to assist you.</p>
                            <a class="btn btn-sm btn-outline-primary" href="/FAQ">Learn More <i class="las la-arrow-right ms-1"></i></a>
                        </div>
                    </div>
                </div>
            </div>
        </section>

        <section class="section try-now overflow-hidden bg-tertiary">
            <div class="container">
                <div class="row justify-content-center">
                    <div class="col-lg-6">
                        <div class="section-title text-center">
                            <p class="text-primary text-uppercase fw-bold mb-3">What are you waiting for?</p>
                            <h1 class="mb-4">Start Using Z-Wallet Today!</h1>
                            <a class="btn btn-primary mt-4" href="/About">Try Z-Wallet Now</a>
                        </div>
                    </div>
                </div>
            </div>
            <div class="has-shapes">
                <svg class="shape shape-left text-light" width="290" height="709" viewBox="0 0 290 709" fill="none"
                    xmlns="http://www.w3.org/2000/svg">
                    <path
                        d="M-119.511 58.4275C-120.188 96.3185 -92.0001 129.539 -59.0325 148.232C-26.0649 166.926 11.7821 174.604 47.8274 186.346C83.8726 198.088 120.364 215.601 141.281 247.209C178.484 303.449 153.165 377.627 149.657 444.969C144.34 546.859 197.336 649.801 283.36 704.673"
                        stroke="currentColor" stroke-miterlimit="10" />
                    <path
                        d="M-141.434 72.0899C-142.111 109.981 -113.923 143.201 -80.9554 161.895C-47.9878 180.588 -10.1407 188.267 25.9045 200.009C61.9497 211.751 98.4408 229.263 119.358 260.872C156.561 317.111 131.242 391.29 127.734 458.631C122.417 560.522 175.414 663.463 261.437 718.335"
                        stroke="currentColor" stroke-miterlimit="10" />
                    <path
                        d="M-163.379 85.7578C-164.056 123.649 -135.868 156.869 -102.901 175.563C-69.9331 194.256 -32.086 201.934 3.9592 213.677C40.0044 225.419 76.4955 242.931 97.4127 274.54C134.616 330.779 109.296 404.957 105.789 472.299C100.472 574.19 153.468 677.131 239.492 732.003"
                        stroke="currentColor" stroke-miterlimit="10" />
                    <path
                        d="M-185.305 99.4208C-185.982 137.312 -157.794 170.532 -124.826 189.226C-91.8589 207.919 -54.0118 215.597 -17.9666 227.34C18.0787 239.082 54.5697 256.594 75.4869 288.203C112.69 344.442 87.3706 418.62 83.8633 485.962C78.5463 587.852 131.542 690.794 217.566 745.666"
                        stroke="currentColor" stroke-miterlimit="10" />
                </svg>
                <svg class="shape shape-right text-light" width="731" height="429" viewBox="0 0 731 429" fill="none"
                    xmlns="http://www.w3.org/2000/svg">
                    <path
                        d="M12.1794 428.14C1.80036 390.275 -5.75764 349.015 8.73984 312.537C27.748 264.745 80.4729 237.968 131.538 231.843C182.604 225.703 234.032 235.841 285.323 239.748C336.615 243.64 391.543 240.276 433.828 210.964C492.452 170.323 511.701 91.1227 564.607 43.2553C608.718 3.35334 675.307 -9.81661 731.29 10.323"
                        stroke="currentColor" stroke-miterlimit="10" />
                    <path
                        d="M51.0253 428.14C41.2045 392.326 34.0538 353.284 47.7668 318.783C65.7491 273.571 115.623 248.242 163.928 242.449C212.248 236.641 260.884 246.235 309.4 249.931C357.916 253.627 409.887 250.429 449.879 222.701C505.35 184.248 523.543 109.331 573.598 64.0588C615.326 26.3141 678.324 13.8532 731.275 32.9066"
                        stroke="currentColor" stroke-miterlimit="10" />
                    <path
                        d="M89.8715 428.14C80.6239 394.363 73.8654 357.568 86.8091 325.028C103.766 282.396 150.788 258.515 196.347 253.054C241.906 247.578 287.767 256.629 333.523 260.099C379.278 263.584 428.277 260.567 465.976 234.423C518.279 198.172 535.431 127.525 582.62 84.8317C621.964 49.2292 681.356 37.4924 731.291 55.4596"
                        stroke="currentColor" stroke-miterlimit="10" />
                    <path
                        d="M128.718 428.14C120.029 396.414 113.678 361.838 125.837 331.274C141.768 291.221 185.939 268.788 228.737 263.659C271.536 258.515 314.621 267.008 357.6 270.282C400.58 273.556 446.607 270.719 482.028 246.16C531.163 212.111 547.275 145.733 591.612 105.635C628.572 72.19 684.375 61.1622 731.276 78.0432"
                        stroke="currentColor" stroke-miterlimit="10" />
                    <path
                        d="M167.564 428.14C159.432 398.451 153.504 366.107 164.863 337.519C179.753 300.046 221.088 279.062 261.126 274.265C301.164 269.452 341.473 277.402 381.677 280.465C421.88 283.527 464.95 280.872 498.094 257.896C544.061 226.035 559.146 163.942 600.617 126.423C635.194 95.1355 687.406 84.8167 731.276 100.612"
                        stroke="currentColor" stroke-miterlimit="10" />
                </svg>
            </div>
        </section>
    </main>
</asp:Content>
