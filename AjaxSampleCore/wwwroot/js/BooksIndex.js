// JavaScript + Partial View

function setResult(html) {
    $('#result').html(html);
}

function loadPartialView() {
    //$("#result").text('読み込み中…');
    //$("#result").load(loadPath, function (response, status, xhr) {
    //    if (status === 'error')
    //        $('#result').text('エラー (status : ' + xhr.status + ', statusText : ' + xhr.statusText + ')');
    //});

    $('#result').text('読み込み中…');
    $.get('/Books/Search/', $('#form1').serialize())
     .done(function (data) { setResult(data); })
     .fail(function (data) { $('#result').text('サーバー エラー'); });
}

$('#form1').submit(function (event) {
    event.preventDefault();
    loadPartialView();
});

// JavaScript + Web API

//function putCommasInEvery3Digits(number) {
//    return number.toString().replace(/(\d)(?=(\d{3})+$)/g, '$1,');
//}

//function toCurrency(number) {
//    return '&yen;' + putCommasInEvery3Digits(number);
//}

function bookToTableRow(book) {
    return $('<tr>')
           .append($('<td><a href="' + book.url + '"><img src="' + book.imageUrl + '" alt="' + book.title + '"></a>' + '</td>'))
           .append($('<td>' + '<a href="' + book.url + '">' + book.title + '</a>' + '</td>'))
           .append($('<td>&yen;' + book.price + '</td>'))
           .append($('<td>' + book.releaseDate + '</td>'))
           .append($('<td>' + book.publisher + '</td>'))
           .append($('<td>' + book.authors + '</td>'));
}

function booksToTable(books) {
    let table = $('<table class="table table-striped">');
    let thead = $('<thead>').appendTo(table);
    let tr    = $('<tr>'   ).appendTo(thead);
    tr.append($('<th></th>'        ));
    tr.append($('<th>タイトル</th>'));
    tr.append($('<th>価格</th>'    ));
    tr.append($('<th>発売日</th>'  ));
    tr.append($('<th>出版社</th>'  ));
    tr.append($('<th>著者</th>'    ));

    let tbody = $('<tbody>').appendTo(table);
    for (var index = 0; index < books.length; index++)
        tbody.append(bookToTableRow(books[index]));
    return table;
}

$('#form2').submit(function (event) {
    event.preventDefault();

    $('#result').text('読み込み中…');

    let searchText = decodeURIComponent($('#searchText').val());
    $.get('/api/BooksApi/' + searchText + '/')
     .done(function (json) { $('#result').html(booksToTable(json)); })
     .fail(function (data) { $('#result').text(''); });
});
