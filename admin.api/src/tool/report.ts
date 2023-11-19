// excelUtils.ts

import * as XLSX from 'xlsx';

export const exportToExcel = (tableData: any[], filename: string, columnHeaders: string[]) => {
    // 构建工作簿
    const wb = XLSX.utils.book_new();

    // 复制数据以便进行修改，以防影响原始数据
    const modifiedData = JSON.parse(JSON.stringify(tableData));

    // 移除不需要导出的列
    modifiedData.forEach(item => delete item.Id);

    // 将修改后的数据转换为工作表
    const ws = XLSX.utils.json_to_sheet(modifiedData);

    // 修改列标题
    XLSX.utils.sheet_add_aoa(ws, [columnHeaders], { origin: 0 });

    // 将工作表添加到工作簿
    XLSX.utils.book_append_sheet(wb, ws, 'Sheet1');

    // 将工作簿转为 ArrayBuffer
    const arrayBuffer = XLSX.write(wb, { bookType: 'xlsx', type: 'array' });

    // 创建 Blob
    const blob = new Blob([arrayBuffer], { type: 'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet' });

    // 创建下载链接
    const url = URL.createObjectURL(blob);
    const a = document.createElement('a');
    a.href = url;
    a.download = `${filename}.xlsx`; // 指定文件名
    document.body.appendChild(a);
    a.click();
    document.body.removeChild(a);
    URL.revokeObjectURL(url);
};
