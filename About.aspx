﻿<%@ Page Title="About Us" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="About.aspx.cs" Inherits="Z_Wallet.About" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <main>
        <section class="page-header bg-tertiary">
            <div class="container">
                <div class="row">
                    <div class="col-8 mx-auto text-center pt-lg-5">
                        <h2 class="mb-3 text-capitalize">About Us</h2>
                        <ul class="list-inline breadcrumbs text-capitalize" style="font-weight: 500">
                            <li class="list-inline-item"><a href="/Default">Home</a>
                            </li>
                            <li class="list-inline-item">/ &nbsp; <a href="/About">About</a>
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
                <div class="row justify-content-center align-items-center">
                    <div class="col-lg-7">
                        <div class="section-title">
                            <p class="text-primary text-uppercase fw-bold mb-3">About Wallet</p>
                            <h2 class="h1 mb-4">Simplify Your Transactions with Our Secure E-Wallet Solution!
                            </h2>
                            <div class="content pe-0 pe-lg-5">
                                <p>
                                    Fast Access to Funds
                                    <br />
                                    Our e-wallet provides quick and easy access to your funds. You can make payments, transfer funds, and receive payments instantly, making it the ideal solution for daily expenses.
                                </p>
                                <p>
                                    Flexible and Convenient
                                    <br />
                                    Our e-wallet offers flexibility and convenience like no other. You can use it to manage your finances on-the-go, making it the perfect solution for busy entrepreneurs and professionals.
                                </p>
                            </div>
                        </div>
                    </div>
                    <div class="col-lg-4 mt-5 mt-lg-0">
                        <img loading="lazy" decoding="async" src="Content/images/about/about-11.jpg" alt="Business Loans &lt;br&gt; For Daily Expenses" class="rounded w-100">
                    </div>
                </div>
            </div>
        </section>

        <section class="about-section section bg-tertiary position-relative overflow-hidden">
            <div class="container">
                <div class="row justify-content-around">
                    <div class="col-lg-5">
                        <div class="section-title">
                            <p class="text-primary text-uppercase fw-bold mb-3"></p>
                            <h2>Who We Are?</h2>
                        </div>
                        <p class="lead">At Insight E-Wallet Advisors, we are a team of independent e-wallet advisors who provide guidance on selecting the right e-wallet for your personal or business needs. Our directory of e-wallet providers gives you all the information you need to make an informed decision.</p>
                        <div class="content">We understand that the world of digital payments can be overwhelming, which is why our team of experts is here to help. We provide unbiased advice and recommendations, so you can choose the e-wallet that works best for you.</div>
                    </div>
                    <div class="col-lg-5">
                        <div class="section-title">
                            <p class="text-primary text-uppercase fw-bold mb-3"></p>
                            <h2>What We Offer?</h2>
                        </div>
                        <p class="lead">At Insight E-Wallet Advisors, we offer a range of services to help you choose the right e-wallet and manage your finances with ease.</p>
                        <div class="content">
                            <ul>
                                <li>Hassle-free E-Wallet Selection  </li>
                                <li>Easy Fund Management</li>
                                <li>Habit-Building with E-Wallets</li>
                            </ul>
                        </div>
                    </div>
                </div>
            </div>
            <div class="has-shapes">
                <svg class="shape shape-left text-light" width="381" height="443" viewBox="0 0 381 443" fill="none" xmlns="http://www.w3.org/2000/svg">
                    <path d="M334.266 499.007C330.108 469.108 304.151 446.496 276.261 435.921C248.372 425.346 218.077 424.035 188.666 419.32C159.254 414.589 128.795 405.375 108.664 383.129C72.8533 343.535 83.3445 282.01 77.7634 228.587C69.3017 147.754 15.4873 73.3967 -58.0001 40.9907" stroke="currentColor" stroke-miterlimit="10" />
                    <path d="M349.584 485.51C345.427 455.611 319.469 433 291.58 422.425C263.69 411.85 233.395 410.538 203.984 405.823C174.573 401.092 144.114 391.878 123.982 369.632C88.1716 330.038 98.6628 268.513 93.0817 215.09C84.62 134.258 30.8056 59.8999 -42.6819 27.494" stroke="currentColor" stroke-miterlimit="10" />
                    <path d="M364.904 472.013C360.747 442.114 334.789 419.503 306.9 408.928C279.011 398.352 248.716 397.041 219.304 392.326C189.893 387.595 159.434 378.381 139.303 356.135C103.492 316.541 113.983 255.016 108.402 201.593C99.9403 120.76 46.1259 46.4028 -27.3616 13.9969" stroke="currentColor" stroke-miterlimit="10" />
                    <path d="M380.24 458.516C376.083 428.617 350.125 406.006 322.236 395.431C294.347 384.856 264.051 383.544 234.64 378.829C205.229 374.098 174.77 364.884 154.639 342.638C118.828 303.044 129.319 241.519 123.738 188.096C115.276 107.264 61.4619 32.906 -12.0255 0.500103" stroke="currentColor" stroke-miterlimit="10" />
                </svg>
                <svg class="shape shape-right text-light" width="406" height="433" viewBox="0 0 406 433" fill="none" xmlns="http://www.w3.org/2000/svg">
                    <path d="M101.974 -86.77C128.962 -74.8992 143.467 -43.2447 146.175 -12.7857C148.883 17.6734 142.273 48.1263 139.087 78.5816C135.916 109.041 136.681 141.702 152.351 167.47C180.247 213.314 240.712 218.81 289.413 238.184C363.095 267.516 418.962 340.253 430.36 421.687" stroke="currentColor" stroke-miterlimit="10" />
                    <path d="M118.607 -98.5031C145.596 -86.6323 160.101 -54.9778 162.809 -24.5188C165.517 5.94031 158.907 36.3933 155.72 66.8486C152.549 97.3082 153.314 129.969 168.985 155.737C196.881 201.581 257.346 207.077 306.047 226.451C379.729 255.783 435.596 328.52 446.994 409.954" stroke="currentColor" stroke-miterlimit="10" />
                    <path d="M135.241 -110.238C162.23 -98.3675 176.735 -66.7131 179.443 -36.254C182.151 -5.79492 175.541 24.6581 172.354 55.1134C169.183 85.573 169.948 118.234 185.619 144.002C213.515 189.846 273.98 195.342 322.681 214.716C396.363 244.048 452.23 316.785 463.627 398.219" stroke="currentColor" stroke-miterlimit="10" />
                    <path d="M151.879 -121.989C178.867 -110.118 193.373 -78.4638 196.081 -48.0047C198.789 -17.5457 192.179 12.9074 188.992 43.3627C185.821 73.8223 186.586 106.483 202.256 132.251C230.153 178.095 290.618 183.591 339.318 202.965C413.001 232.297 468.867 305.034 480.265 386.468" stroke="currentColor" stroke-miterlimit="10" />
                </svg>
            </div>
        </section>

        <section class="section-sm bg-primary-light">
            <div class="container">
                <div class="row justify-content-center">
                    <div class="col-lg-4 col-md-6 mb-5 mb-lg-0 text-center icon-box-item">
                        <div class="icon icon-lg bg-transparent mb-4">
                            <i class="fas fa-mouse-pointer text-primary"></i>
                        </div>
                        <h3>Seamless Sign-up</h3>
                        <p class="px-lg-5">With our e-wallet, sign-up is easy and can be done at your convenience. Simply download the app and follow the simple steps to set up your account.</p>
                    </div>
                    <div class="col-lg-4 col-md-6 mb-5 mb-lg-0 text-center icon-box-item">
                        <div class="icon icon-lg bg-transparent mb-4">
                            <i class="fas fa-file-alt text-primary"></i>
                        </div>
                        <h3>Effortless Account Activation</h3>
                        <p class="px-lg-5">Our account activation process is designed to be simple and hassle-free. With just a few clicks, you can activate your account and start using your e-wallet to manage your finances.</p>
                    </div>
                    <div class="col-lg-4 col-md-6 mb-5 mb-lg-0 text-center icon-box-item">
                        <div class="icon icon-lg bg-transparent mb-4">
                            <i class="fas fa-briefcase text-primary"></i>
                        </div>
                        <h3>Funds on the Go</h3>
                        <p class="px-lg-5">With our e-wallet, you can access your funds anytime, anywhere. You can make payments, transfer funds, and manage your finances on-the-go, all with just a few taps on your browser.</p>
                    </div>
                </div>
            </div>
        </section>

        <section class="section core-value bg-tertiary">
            <div class="container">
                <div class="row align-items-center">
                    <div class="col-lg-6">
                        <div class="row position-relative gy-4">
                            <div class="icon-box-item col-md-6">
                                <div class="block bg-white">
                                    <div class="icon rounded-number">01</div>
                                    <h3 class="mb-3">Authentic</h3>
                                    <p class="mb-0">We prioritize transparency and honesty in all our dealings, ensuring that our customers receive genuine and reliable service.</p>
                                </div>
                            </div>
                            <div class="icon-box-item col-md-6">
                                <div class="block bg-white">
                                    <div class="icon rounded-number">02</div>
                                    <h3 class="mb-3">Empathetic</h3>
                                    <p class="mb-0">We strive to uphold the highest standards of integrity and honesty in all our interactions with customers and partners.</p>
                                </div>
                            </div>
                            <div class="icon-box-item col-md-6">
                                <div class="block bg-white">
                                    <div class="icon rounded-number">03</div>
                                    <h3 class="mb-3">All Improving</h3>
                                    <p class="mb-0">We constantly strive to enhance our e-wallet with the latest technology and features, ensuring that our users have the best possible experience.</p>
                                </div>
                            </div>
                            <div class="icon-box-item col-md-6">
                                <div class="block bg-white">
                                    <div class="icon rounded-number">04</div>
                                    <h3 class="mb-3">User-Focused</h3>
                                    <p class="mb-0">Our e-wallet is designed with our users in mind, providing a simple, intuitive interface and features that cater to their needs.</p>
                                </div>
                            </div>
                            <div class="has-shapes">
                                <svg class="shape shape-1 text-primary" width="71" height="71" viewBox="0 0 119 119" fill="none" xmlns="http://www.w3.org/2000/svg">
                                    <path d="M8.50598 89.8686C8.17023 89.3091 7.83449 88.6376 7.49875 88.078L66.0305 0.336418C66.7019 0.448334 67.3734 0.560249 68.0449 0.560249L8.50598 89.8686Z" fill="currentColor" />
                                    <path d="M5.03787 83.2646C4.70213 82.5932 4.47829 81.9217 4.14255 81.2502L58.3096 -0.00032826C59.093 -0.000328191 59.7645 -0.000328132 60.5479 -0.000328064L5.03787 83.2646Z" fill="currentColor" />
                                    <path d="M16.9007 100.613C16.453 100.165 16.0053 99.7175 15.5577 99.2698L79.4613 3.47031C80.0209 3.69414 80.6924 3.91795 81.252 4.14178L16.9007 100.613Z" fill="currentColor" />
                                    <path d="M12.5352 95.5762C12.0876 95.0166 11.7518 94.5689 11.3042 94.0094L72.9695 1.45541C73.641 1.56732 74.2006 1.79115 74.8721 1.90306L12.5352 95.5762Z" fill="currentColor" />
                                    <path d="M0.00101471 55.5103C0.11293 54.1673 0.224831 52.9362 0.336747 51.5932L29.6586 7.72242C30.7777 7.05093 31.8969 6.49136 33.1279 5.93178L0.00101471 55.5103Z" fill="currentColor" />
                                    <path d="M26.1887 108.334C25.9649 108.223 25.7411 107.999 25.5172 107.887L91.2115 9.40136C91.4353 9.51328 91.6592 9.7371 91.883 9.84901C92.2188 10.0728 92.4426 10.2967 92.7783 10.4086L27.084 108.894C26.8602 108.67 26.5245 108.558 26.1887 108.334Z" fill="currentColor" />
                                    <path d="M114.042 81.0269C112.587 84.7201 110.685 88.4133 108.334 91.8827C105.984 95.3521 103.41 98.4857 100.5 101.396L114.042 81.0269Z" fill="currentColor" />
                                    <path d="M0.335842 66.7012C0.223927 65.6939 0.112026 64.7986 0.000110881 63.7914L40.7373 2.79753C41.6326 2.46179 42.6398 2.23796 43.5352 2.01413L0.335842 66.7012Z" fill="currentColor" />
                                    <path d="M2.23929 75.6538C2.01546 74.8704 1.79162 74.087 1.56779 73.3036L50.0271 0.558655C50.8105 0.446747 51.7059 0.334824 52.4893 0.222908L2.23929 75.6538Z" fill="currentColor" />
                                    <path d="M32.793 112.139C32.2335 111.915 31.6739 111.58 31.1143 111.244L96.4728 13.206C96.9205 13.6537 97.4801 13.9894 97.9277 14.4371L32.793 112.139Z" fill="currentColor" />
                                    <path d="M77.7822 115.161C76.8868 115.497 75.8796 115.72 74.9843 116.056L117.848 51.8168C117.96 52.824 118.072 53.7193 118.184 54.7266L77.7822 115.161Z" fill="currentColor" />
                                    <path d="M68.493 117.512C67.7096 117.624 66.8143 117.736 66.0309 117.848L116.057 42.8644C116.281 43.6478 116.505 44.4312 116.729 45.3265L68.493 117.512Z" fill="currentColor" />
                                    <path d="M60.0992 118.294C59.3158 118.294 58.6443 118.294 57.8609 118.294L113.259 35.2533C113.595 35.9248 113.819 36.5963 114.154 37.2678L60.0992 118.294Z" fill="currentColor" />
                                    <path d="M21.8245 105.087C21.3768 104.64 20.8172 104.304 20.3696 103.856L85.6162 6.15427C86.1758 6.37809 86.7354 6.71384 87.2949 7.04959L21.8245 105.087Z" fill="currentColor" />
                                    <path d="M89.0856 110.124C87.9665 110.795 86.7354 111.467 85.6162 112.026L118.184 63.1194C118.072 64.4624 117.96 65.8054 117.736 67.0364L89.0856 110.124Z" fill="currentColor" />
                                    <path d="M3.69339 38.2759C5.2602 34.135 7.27468 30.1061 9.84873 26.189C12.4228 22.3839 15.3326 18.9145 18.5781 15.8928L3.69339 38.2759Z" fill="currentColor" />
                                    <path d="M52.49 117.848C51.8185 117.736 51.147 117.736 50.4755 117.624L109.791 28.5392C110.126 29.0988 110.462 29.7703 110.798 30.3299L52.49 117.848Z" fill="currentColor" />
                                    <path d="M38.9475 114.712C38.388 114.489 37.7165 114.265 37.1569 114.041L101.396 17.6818C101.844 18.1295 102.292 18.5771 102.739 19.0248L38.9475 114.712Z" fill="currentColor" />
                                    <path d="M45.4392 116.728C44.7677 116.616 44.2081 116.392 43.5366 116.28L105.873 22.8306C106.321 23.3902 106.657 23.8378 107.105 24.3974L45.4392 116.728Z" fill="currentColor" />
                                </svg>
                                <svg class="shape shape-2 text-primary" width="100" height="100" viewBox="0 0 119 119" fill="none" xmlns="http://www.w3.org/2000/svg">
                                    <path d="M8.50598 89.8686C8.17023 89.3091 7.83449 88.6376 7.49875 88.078L66.0305 0.336418C66.7019 0.448334 67.3734 0.560249 68.0449 0.560249L8.50598 89.8686Z" fill="currentColor" />
                                    <path d="M5.03787 83.2646C4.70213 82.5932 4.47829 81.9217 4.14255 81.2502L58.3096 -0.00032826C59.093 -0.000328191 59.7645 -0.000328132 60.5479 -0.000328064L5.03787 83.2646Z" fill="currentColor" />
                                    <path d="M16.9007 100.613C16.453 100.165 16.0053 99.7175 15.5577 99.2698L79.4613 3.47031C80.0209 3.69414 80.6924 3.91795 81.252 4.14178L16.9007 100.613Z" fill="currentColor" />
                                    <path d="M12.5352 95.5762C12.0876 95.0166 11.7518 94.5689 11.3042 94.0094L72.9695 1.45541C73.641 1.56732 74.2006 1.79115 74.8721 1.90306L12.5352 95.5762Z" fill="currentColor" />
                                    <path d="M0.00101471 55.5103C0.11293 54.1673 0.224831 52.9362 0.336747 51.5932L29.6586 7.72242C30.7777 7.05093 31.8969 6.49136 33.1279 5.93178L0.00101471 55.5103Z" fill="currentColor" />
                                    <path d="M26.1887 108.334C25.9649 108.223 25.7411 107.999 25.5172 107.887L91.2115 9.40136C91.4353 9.51328 91.6592 9.7371 91.883 9.84901C92.2188 10.0728 92.4426 10.2967 92.7783 10.4086L27.084 108.894C26.8602 108.67 26.5245 108.558 26.1887 108.334Z" fill="currentColor" />
                                    <path d="M114.042 81.0269C112.587 84.7201 110.685 88.4133 108.334 91.8827C105.984 95.3521 103.41 98.4857 100.5 101.396L114.042 81.0269Z" fill="currentColor" />
                                    <path d="M0.335842 66.7012C0.223927 65.6939 0.112026 64.7986 0.000110881 63.7914L40.7373 2.79753C41.6326 2.46179 42.6398 2.23796 43.5352 2.01413L0.335842 66.7012Z" fill="currentColor" />
                                    <path d="M2.23929 75.6538C2.01546 74.8704 1.79162 74.087 1.56779 73.3036L50.0271 0.558655C50.8105 0.446747 51.7059 0.334824 52.4893 0.222908L2.23929 75.6538Z" fill="currentColor" />
                                    <path d="M32.793 112.139C32.2335 111.915 31.6739 111.58 31.1143 111.244L96.4728 13.206C96.9205 13.6537 97.4801 13.9894 97.9277 14.4371L32.793 112.139Z" fill="currentColor" />
                                    <path d="M77.7822 115.161C76.8868 115.497 75.8796 115.72 74.9843 116.056L117.848 51.8168C117.96 52.824 118.072 53.7193 118.184 54.7266L77.7822 115.161Z" fill="currentColor" />
                                    <path d="M68.493 117.512C67.7096 117.624 66.8143 117.736 66.0309 117.848L116.057 42.8644C116.281 43.6478 116.505 44.4312 116.729 45.3265L68.493 117.512Z" fill="currentColor" />
                                    <path d="M60.0992 118.294C59.3158 118.294 58.6443 118.294 57.8609 118.294L113.259 35.2533C113.595 35.9248 113.819 36.5963 114.154 37.2678L60.0992 118.294Z" fill="currentColor" />
                                    <path d="M21.8245 105.087C21.3768 104.64 20.8172 104.304 20.3696 103.856L85.6162 6.15427C86.1758 6.37809 86.7354 6.71384 87.2949 7.04959L21.8245 105.087Z" fill="currentColor" />
                                    <path d="M89.0856 110.124C87.9665 110.795 86.7354 111.467 85.6162 112.026L118.184 63.1194C118.072 64.4624 117.96 65.8054 117.736 67.0364L89.0856 110.124Z" fill="currentColor" />
                                    <path d="M3.69339 38.2759C5.2602 34.135 7.27468 30.1061 9.84873 26.189C12.4228 22.3839 15.3326 18.9145 18.5781 15.8928L3.69339 38.2759Z" fill="currentColor" />
                                    <path d="M52.49 117.848C51.8185 117.736 51.147 117.736 50.4755 117.624L109.791 28.5392C110.126 29.0988 110.462 29.7703 110.798 30.3299L52.49 117.848Z" fill="currentColor" />
                                    <path d="M38.9475 114.712C38.388 114.489 37.7165 114.265 37.1569 114.041L101.396 17.6818C101.844 18.1295 102.292 18.5771 102.739 19.0248L38.9475 114.712Z" fill="currentColor" />
                                    <path d="M45.4392 116.728C44.7677 116.616 44.2081 116.392 43.5366 116.28L105.873 22.8306C106.321 23.3902 106.657 23.8378 107.105 24.3974L45.4392 116.728Z" fill="currentColor" />
                                </svg>
                            </div>
                        </div>
                    </div>
                    <div class="col-lg-6 mt-5 mt-lg-0">
                        <div class="section-title ps-0 ps-lg-5">
                            <p class="text-primary text-uppercase fw-bold mb-3">Values We Provide</p>
                            <h2 class="h1">Our Core Values</h2>
                            <div class="content">
                                <p>1. Trust: We are dedicated to building trust with our users through transparency, reliability, and honesty.</p>
                                <p>2. Innovation: We continuously strive to improve our e-wallet through innovation and cutting-edge technology, providing our users with the best possible experience.</p>
                                <p>3. Security: We take the security of our users' information and transactions seriously, and we have implemented rigorous measures to ensure the safety and privacy of our users.</p>
                                <p>4. Accessibility: We believe that financial services should be accessible to everyone, and we are committed to providing a platform that is user-friendly and easy to navigate.</p>
                                <p>5. Customer Focus: We put our users at the center of everything we do, and we are dedicated to providing personalized solutions and exceptional customer service to meet their unique needs.</p>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </section>

        <section class="section teams">
            <div class="container">
                <div class="row justify-content-center">
                    <div class="col-12">
                        <div class="section-title text-center">
                            <p class="text-primary text-uppercase fw-bold mb-3">Behind Us</p>
                            <h1>People Behind Us</h1>
                            <p class="mb-0">
                                Our team is made up of passionate and skilled professionals who are dedicated to providing the best possible service to our users.
						<br>
                                Together, we work towards our mission of creating a seamless and secure digital experience for all.
                            </p>
                        </div>
                    </div>
                </div>
                <div class="row position-relative justify-content-center">
                    <div class="col-xl-3 col-lg-4 col-md-6 mt-4">
                        <div class="card bg-transparent border-0 text-center">
                            <div class="card-img">
                                <img loading="lazy" decoding="async" src="Content/images/about/team-1.jpg" alt="Zildjan Leenor Luvindino" class="rounded w-100" width="300" height="332">
                                <ul class="card-social list-inline">
                                    <li class="list-inline-item"><a class="facebook" href="https://www.facebook.com/mr.zildjan" target="_blank" rel="noopener noreferrer"><i class="fab fa-facebook"></i></a>
                                    </li>
                                    <li class="list-inline-item"><a class="twitter" href="https://github.com/mrzildjan" target="_blank" rel="noopener noreferrer"><i class="fab fa-github"></i></a>
                                    </li>
                                    <li class="list-inline-item"><a class="instagram" href="https://www.instagram.com/zildjan.leenor/" target="_blank" rel="noopener noreferrer"><i class="fab fa-instagram"></i></a>
                                    </li>
                                </ul>
                            </div>
                            <div class="card-body">
                                <h3>Zildjan Luvindino</h3>
                                <p>Developer</p>
                            </div>
                        </div>
                    </div>
                    <div class="col-xl-3 col-lg-4 col-md-6 mt-4">
                        <div class="card bg-transparent border-0 text-center">
                            <div class="card-img">
                                <img loading="lazy" decoding="async" src="Content/images/about/team-2.jpg" alt="Jesyl Labajo" class="rounded w-100" width="300" height="333">
                                <ul class="card-social list-inline">
                                    <li class="list-inline-item"><a class="facebook" href="https://www.facebook.com/jesyl.labajo" target="_blank" rel="noopener noreferrer"><i class="fab fa-facebook"></i></a>
                                    </li>
                                    <li class="list-inline-item"><a class="instagram" href="https://www.instagram.com/jsyllabajo18/"><i class="fab fa-instagram" target="_blank" rel="noopener noreferrer"></i></a>
                                    </li>
                                </ul>
                            </div>
                            <div class="card-body">
                                <h3>Jesyl Labajo</h3>
                                <p>Founder</p>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </section>
    </main>
</asp:Content>
