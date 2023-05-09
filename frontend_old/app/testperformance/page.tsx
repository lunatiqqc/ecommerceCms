import Script from "next/script";

export default function page() {
    return (
        <div>
            {" "}
            <h1>Static File Performance Test</h1>
            <p>
                This page contains some static files (a JavaScript file and a
                CSS file) that are being served by your server. The following
                table shows the time it took for your server to serve these
                files:
            </p>
            <table>
                <tr>
                    <th>File</th>
                    <th>Size</th>
                    <th>Time</th>
                </tr>
                <tr>
                    <td>jquery.min.js</td>
                    <td>87 KB</td>
                    <td id='jquery-time'>Calculating...</td>
                </tr>
                <tr>
                    <td>style.css</td>
                    <td>3 KB</td>
                    <td id='css-time'>Calculating...</td>
                </tr>
            </table>
            <Script>
                {" "}
                {`
      // Measure the time it takes to load the jQuery file
      var jqueryStartTime = performance.now();
      $.getScript('/static/js/jquery.min.js', function() {
        var jqueryEndTime = performance.now();
        var jqueryTime = jqueryEndTime - jqueryStartTime;
        $('#jquery-time').html(jqueryTime.toFixed(2) + ' ms');
      });
  
      // Measure the time it takes to load the CSS file
      var cssStartTime = performance.now();
      var cssLink = document.createElement('link');
      cssLink.rel = 'stylesheet';
      cssLink.href = '/static/css/style.css';
      cssLink.onload = function() {
        var cssEndTime = performance.now();
        var cssTime = cssEndTime - cssStartTime;
        $('#css-time').html(cssTime.toFixed(2) + ' ms');
      };
      document.head.appendChild(cssLink);
      `}
            </Script>
        </div>
    );
}
