<template>
    <el-card class="box-card">
      <!-- 其他内容... -->
  
      <!-- 分页组件 -->
      <el-pagination
        @size-change="handleSizeChange"
        @current-change="handleCurrentChange"
        :current-page="currentPage"
        :page-sizes="[10, 20, 30, 40]"
        :page-size="pageSize"
        :total="totalItems"
      />
  
      <!-- 弹出框组件... -->
    </el-card>
  </template>


<script lang="ts" setup>
// ... 其他引入和声明

const currentPage = ref(1);  // 当前页
const pageSize = ref(10);    // 每页显示条目数
const totalItems = ref(0);   // 总条目数

// 处理分页大小变化
const handleSizeChange = (val) => {
  pageSize.value = val;
  load();
};

// 处理页码变化
const handleCurrentChange = (val) => {
  currentPage.value = val;
  load();
};

// ... 其他逻辑
</script>
